using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HARD.CORE.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
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
        /// <param name="claveUsuario">The unique identifier of the user.</param>
        /// <returns>
        ///     The user information if found; otherwise, an error message.
        /// </returns>
        [HttpGet("Obtener")]
        [AllowAnonymous]
        public IActionResult Obtener([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<Usuario>();
            try
            {
                string decodedClaveUsuario = Uri.UnescapeDataString(claveUsuario);
                webResult.Data = _usuarioB.Obtener(decodedClaveUsuario);
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
        /// <param name="estatus">The status filter for users.</param>
        /// <returns>
        ///     A list of all users if found; otherwise, an error message.
        /// </returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos([FromQuery] bool? estatus = null)
        {
            var webResult = new WebResultModel<List<Usuario>>();
            try
            {
                webResult.Data = _usuarioB.ObtenerTodos();
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
        /// Gets all users from the Active Directory.
        /// </summary>
        /// <returns>
        ///     A list of users from the Active Directory if found; otherwise, an error message.
        /// </returns>  
        [HttpGet("ObtenerUsuariosDirectorioActivo")]
        public IActionResult ObtenerUsuariosDirectorioActivo()
        {
            var webResult = new WebResultModel<List<Usuario>>();
            try
            {
                webResult.Data = _usuarioB.ObtenerUsuariosDirectorioActivo();
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
        /// Gets a user from the Active Directory by user key.
        /// </summary>
        /// <param name="claveUsuario">The unique identifier of the user.</param>
        /// <returns>
        ///     The user information if found; otherwise, an error message.
        /// </returns>
        [HttpGet("ObtenerUsuarioDirectorioActivo")]
        public IActionResult ObtenerUsuarioDirectorioActivo([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<Usuario>();
            try
            {
                string decodedClaveUsuario = Uri.UnescapeDataString(claveUsuario);
                webResult.Data = (Usuario)_usuarioB.ObtenerUsuarioDirectorioActivo(decodedClaveUsuario);
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
        /// Gets a user from the Active Directory by user key.
        /// </summary>
        /// <param name="claveUsuario">The unique identifier of the user.</param>
        /// <returns>
        ///     The user information if found; otherwise, an error message.
        /// </returns>
        [HttpGet("ObtenerUsuariosDirectorioActivoTodos")]
        public IActionResult ObtenerUsuariosDirectorioActivoTodos()
        {
            var webResult = new WebResultModel<List<Usuario>>();
            try
            {
                webResult.Data = _usuarioB.ObtenerUsuariosDirectorioActivoTodos();
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
        /// Gets a suggested user from the Active Directory based on the provided names.
        /// </summary>
        /// <param name="apellidoPaterno">The last name of the user.</param>
        /// <param name="apellidoMaterno">The mother's last name of the user.</param>
        /// <param name="nombres">The first name of the user.</param>
        /// <returns>
        ///     The suggested user information if found; otherwise, an error message.
        /// </returns>
        [HttpGet("ObtenerUsuarioSugerido")]
        public IActionResult ObtenerUsuarioSugerido([FromQuery, Required] string apellidoPaterno, [FromQuery, Required] string apellidoMaterno, [FromQuery, Required] string nombres)
        {
            var webResult = new WebResultModel<string>();
            try
            {
                string decodedApellidoPaterno = Uri.UnescapeDataString(apellidoPaterno);
                string decodedApellidoMaterno = Uri.UnescapeDataString(apellidoMaterno);
                string decodedNombres = Uri.UnescapeDataString(nombres);
                webResult.Data = _usuarioB.ObtenerUsuarioSugerido(apellidoPaterno: decodedApellidoPaterno, apellidoMaterno: decodedApellidoMaterno, nombres: decodedNombres);
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
        /// Gets the activity information for a user.
        /// </summary>
        /// <param name="claveUsuario">
        /// The unique identifier of the user.
        /// </param>
        /// <param name="fechaInicial">
        /// The start date for the activity search.
        /// </param>
        /// <param name="fechaFinal">
        /// The end date for the activity search.
        /// </param>
        /// <returns>
        /// The activity information if found; otherwise, an error message.
        /// </returns>
        [HttpGet("ObtenerActividad")]
        public IActionResult ObtenerActividad([FromQuery] string? claveUsuario = null, [FromQuery] DateTime? fechaInicial = null, [FromQuery] DateTime? fechaFinal = null)
        {
            var webResult = new WebResultModel<List<BitacoraAcessos>>();
            try
            {
                webResult.Data = _usuarioB.ObtenerActividad(claveUsuario: claveUsuario, fechaInicial: fechaInicial, fechaFinal: fechaFinal);
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
        /// Gets the detailed activity information for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique identifier of the user.</param>
        /// <returns>
        ///     The detailed activity information if found; otherwise, an error message.
        /// </returns>
        [HttpGet("ObtenerDetalleActividad")]
        public IActionResult ObtenerDetalleActividad([FromQuery, Required] string claveUsuario, [FromQuery] DateTime? fechaInicial = null, [FromQuery] DateTime? fechaFinal = null)
        {
            var webResult = new WebResultModel<List<BitacoraAcessos>>();
            try
            {
                webResult.Data = _usuarioB.ObtenerActividad(claveUsuario: claveUsuario, fechaInicial: fechaInicial, fechaFinal: fechaFinal);
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
        /// Gets the password change eligibility for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique identifier of the user.</param>
        /// <returns>
        ///     True if the user can change their password; otherwise, false.
        /// </returns>
        [AllowAnonymous]
        [HttpGet("PuedeCambiarContrasena")]
        public IActionResult PuedeCambiarContrasena([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                string decodeClaveUsuario = Uri.UnescapeDataString(claveUsuario);
                webResult.Data = _usuarioB.PuedeCambiarContrasena(claveUsuario);
                webResult.Message = "Validación realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al validar si el usuario puede cambiar la contraseña.";
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
        [HttpGet("ExisteUsuario")]
        public IActionResult ExisteUsuario([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<bool>();
            webResult.Message = "Error al validar existencia del usuario.";
            try
            {
                string decodeClaveUsuario = Uri.UnescapeDataString(claveUsuario);
                Usuario usuario = _usuarioB.Obtener(claveUsuario: decodeClaveUsuario);
                if (string.IsNullOrEmpty(usuario.ClaveUsuario))
                {
                    webResult.Data = false;
                    webResult.Message = "El usuario no existe en el sistema.";

                }
                else
                {
                    webResult.Data = true;
                    webResult.Message = "El usuario existe en el sistema.";
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
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] Usuario usuario)
        {
            var webResult = new WebResultModel<Usuario>();
            try
            {
                string defaultPassword = _config["DefaultPassword"] ?? "Default.123@";
                usuario.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                usuario.ClaveUsuarioAlta = ClaveUsuario;
                _usuarioB.Insertar(usuario: usuario, defaultPassword: defaultPassword);
                webResult.Data = (Usuario)_usuarioB.Obtener(claveUsuario: usuario.ClaveUsuario);
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
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Usuario usuario)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                usuario.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _usuarioB.Actualizar(usuario: usuario);
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
        /// Gets the activity registration status for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique identifier of the user.</param>
        /// <param name="tipoRegistro">The type of activity registration.</param>
        /// <returns>
        ///     The activity registration status if found; otherwise, an error message.
        /// </returns>
        [HttpGet("RegistrarActividad")]
        public IActionResult RegistrarActividad([FromQuery, Required] string claveUsuario, [FromQuery, Required] int tipoRegistro)
        {
            var webResult = new WebResultModel<object>();
            try
            {
                string decodedClaveUsuario = Uri.UnescapeDataString(claveUsuario);
                BitacoraEventos bitacoraEventos = new BitacoraEventos();
                bitacoraEventos.ClaveUsuario = decodedClaveUsuario;
                _usuarioB.RegistrarActividad(bitacoraEventos, tipoRegistro);
                webResult.Message = "Registro de la actividad del usuario realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar el registro de la actividad del usuario.";
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
        [HttpPost("Autenticar")]
        public IActionResult Autenticar([FromBody] Login login)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _usuarioB.AutenticarUsuario(claveUsuario: login.Username, password: login.Password);
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
        [HttpPut("ActualizaContrasena")]
        public IActionResult ActualizaContrasena([FromBody] Login login)
        {
            var webResult = new WebResultModel<bool>();
            webResult.Data = false;
            try
            {

                webResult.Message = "Actualización de contraseña realizada exitosamente.";
                Usuario usuario = new Usuario();
                usuario.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                usuario.ClaveUsuario = login.Username;
                usuario.Contrasena = login.Password;
                webResult.Data = _usuarioB.ActualizaContrasena(usuario: usuario);
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
        [HttpPut("Desbloquear")]
        public IActionResult Desbloquear([FromBody, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<bool>();
            webResult.Data = false;
            try
            {
                webResult.Message = "Desbloqueo de usuario realizado exitosamente.";
                Usuario usuario = new Usuario();
                usuario.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                usuario.ClaveUsuario = claveUsuario;
                webResult.Data = _usuarioB.Desbloquear(usuario: usuario);
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar el desbloqueo de usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates the user's password.
        /// </summary>
        /// <param name="claveUsuario">The user's key.</param>
        /// <returns>True if the password is updated successfully; otherwise, false.</returns>
        [AllowAnonymous]
        [HttpGet("ActualizaIntento")]
        public IActionResult ActualizaIntento([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<object>();
            try
            {
                string decodedClaveUsuario = Uri.UnescapeDataString(claveUsuario);
                _usuarioB.ActualizaIntento(claveUsuario: decodedClaveUsuario);
                webResult.Message = "Actualización de intento de ingreso realizado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el intento de ingreso del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
