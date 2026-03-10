using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HARD.CORE.API.Controllers.V2
{
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    /// <summary>
    /// Controller for user management.
    /// </summary>  
    public class UsuarioController : BaseController
    {

        private readonly IConfiguration _config;
        private readonly IUsuarioB _usuarioB;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsuarioController"/> class.
        /// </summary>
        /// <param name="config">The configuration settings for the application.</param>
        /// <param name="usuarioB">The user business logic layer.</param>
        public UsuarioController(IConfiguration config, IUsuarioB usuarioB)
        {
            _config = config;
            _usuarioB = usuarioB;
        }

        /// <summary>
        /// Gets user information by user key.
        /// </summary>
        /// <param name="idUsuario">The unique identifier of the user.</param>
        /// <returns>
        ///     The user information if found; otherwise, an error message.
        /// </returns>
        [HttpGet("GetById")]
        [AllowAnonymous]
        public IActionResult GetById([FromQuery, Required] int idUsuario)
        {
            var webResult = new WebResultModel<Usuario>();
            try
            {
                webResult.Data = _usuarioB.GetById(idUsuario);
                webResult.Message = "Información obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>   
        /// <param name="activo">The status filter for users.</param>
        /// <returns>
        ///     A list of all users if found; otherwise, an error message.
        /// </returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] bool? activo = null)
        {
            var webResult = new WebResultModel<List<Usuario>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    Filters = new BaseFilter
                    {
                        Activo = activo
                    }
                };
                webResult.Data = _usuarioB.GetAll(pagedFilter).ToList();
                webResult.Message = "Información obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Checks if a user exists in the system.
        /// </summary>
        /// <param name="claveUsuario">
        ///     The unique identifier of the user.
        /// </param>
        /// <returns>
        /// True if the user exists; otherwise, false.
        /// </returns>
        [AllowAnonymous]
        [HttpGet("Exists")]
        public IActionResult Exists([FromQuery, Required] int idUsuario)
        {
            var webResult = new WebResultModel<bool>();
            webResult.Message = "Error al validar existencia del usuario.";
            try
            {
                if (_usuarioB.Exists(idUsuario))
                {
                    webResult.Data = true;
                    webResult.Message = "El usuario existe en el sistema.";
                }
                else
                {
                    webResult.Data = false;
                    webResult.Message = "El usuario no existe en el sistema.";
                }
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Gets the detailed activity information for a user.
        /// </summary>
        /// <param name="usuario">The user information.</param>
        /// <returns>
        ///     The detailed activity information if found; otherwise, an error message.
        /// </returns>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Usuario usuario)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                string defaultPassword = _config["DefaultPassword"] ?? "Default.123@";
                usuario.Contrasena = defaultPassword;
                usuario.IdUsuarioModificacion = IdUsuario;
                usuario.IdUsuarioCreacion = IdUsuario;
                webResult.Data = _usuarioB.Add(usuario);
                webResult.Message = "Inserción realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la inserción.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates the user information.
        /// </summary>
        /// <param name="usuario">
        /// The user information to update.
        /// </param>
        /// <returns>
        /// A result indicating the success or failure of the update operation.
        /// </returns>
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                usuario.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _usuarioB.Update(usuario);
                webResult.Message = webResult.Data ? "Actualización realizada exitosamente." : "No se realizó ninguna actualización.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la actualización.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="login">The login information.</param>
        /// <returns>
        ///     True if the user is authenticated; otherwise, false.
        /// </returns>
        [HttpPost("AuthenticateUser")]
        public IActionResult AuthenticateUser([FromBody] Login login)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                string defaultUser = _config["DefaultUser"] ?? "administrador";
                string defaultPassword = _config["DefaultPassword"] ?? "Default.123@";
                if (login.Username == defaultUser && login.Password == defaultPassword)
                {
                    webResult.Data = true;
                    webResult.Message = "Autenticación realizada exitosamente con usuario predeterminado.";
                    webResult.Success = true;
                    return Ok(webResult);
                }

                Usuario usuario = _usuarioB.GetByUsername(login.Username);
                bool isAuthenticated = _usuarioB.AuthenticateUser(usuario.Id, login.Password);
                if (!isAuthenticated)
                {
                    webResult.Data = false;
                    webResult.Message = "Usuario o contraseña incorrectos.";
                    webResult.Success = false;
                    return Ok(webResult);
                }

                webResult.Data = true;
                webResult.Message = "Autenticación realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la autenticación.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates the user's password.
        /// </summary>
        /// <param name="login">The login information.</param>
        /// <returns>
        ///     True if the password is updated successfully; otherwise, false.
        /// </returns>
        /// 
        [AllowAnonymous]
        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] Login login)
        {
            var webResult = new WebResultModel<bool>();
            webResult.Data = false;
            try
            {
                webResult.Message = "Actualización de contraseña realizada exitosamente.";
                Usuario usuario = new Usuario
                {
                    IdUsuarioModificacion = IdUsuario,
                    ClaveUsuario = login.Username,
                    Contrasena = login.Password
                };
                webResult.Data = _usuarioB.UpdatePassword(usuario: usuario);
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la actualización de contraseña.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Unlocks a user.
        /// </summary>
        /// <param name="claveUsuario">The user's key.</param>
        /// </param>
        /// <returns>
        ///     True if the user is unlocked successfully; otherwise, false.
        /// </returns>
        [HttpPut("UnlockUser")]
        public IActionResult UnlockUser([FromBody, Required] int idUsuario)
        {
            var webResult = new WebResultModel<bool>();
            webResult.Data = false;
            try
            {
                webResult.Message = "Desbloqueo de usuario realizado exitosamente.";
                Usuario usuario = _usuarioB.GetById(idUsuario);
                usuario.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _usuarioB.UnlockUser(usuario: usuario);
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar el desbloqueo de usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
