using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Helpers;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HARD.CORE.API.Controllers.V1
{
    /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for managing user profiles.
    /// </remarks>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    [ApiController]
    public class PerfilController : BaseController
    {
          
        private readonly IPerfilB _perfilB;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerfilController"/> class.
        /// </summary>
        /// <param name="perfilB">
        /// The profile business logic layer.
        /// </param>
        public PerfilController(IPerfilB perfilB)
        {
            _perfilB = perfilB;
        }

        /// <summary>
        /// Obtains a profile by its unique key.
        /// </summary>
        /// <param name="clavePerfil">The unique key identifying the profile.</param>
        /// <returns>The profile associated with the provided key.</returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int clavePerfil)
        {
            var webResult = new WebResultModel<Perfil>();
            try
            {
                webResult.Data = (Perfil)_perfilB.Obtener(clavePerfil);
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
        /// Obtains all profiles.
        /// </summary>
        /// <returns>A list of all profiles.</returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos([FromQuery] bool? estatus = null)
        {
            var webResult = new WebResultModel<List<Perfil>>();
            try
            {
                webResult.Data = _perfilB.ObtenerTodos(estatus: estatus);
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
        /// Obtains all profiles assigned to a specific user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>A list of profiles assigned to the specified user.</returns>
        [HttpGet("ObtenerAsignado")]
        public IActionResult ObtenerAsignado([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<List<Perfil>>();
            try
            {
                webResult.Data = _perfilB.ObtenerTodos(claveUsuario: claveUsuario);
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
        /// Obtains all active profiles.
        /// </summary>
        /// <returns>A list of all active profiles.</returns>
        [HttpGet("ObtenerActivos")]
        public IActionResult ObtenerActivos()
        {
            var webResult = new WebResultModel<List<Perfil>>();
            try
            {
                webResult.Data = _perfilB.ObtenerTodos(estatus: true);
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
        /// Inserts a new profile.
        /// </summary>
        /// <param name="perfil">The profile to insert.</param>
        /// <returns>The unique key of the inserted profile.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] Perfil perfil)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                perfil.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _perfilB.Insertar(perfil);
                webResult.Message = "Inserción exitosa del perfil.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al insertar perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates an existing profile.
        /// </summary>
        /// <param name="perfil">The profile to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Perfil perfil)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                perfil.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _perfilB.Actualizar(perfil);
                webResult.Message = "Actualización exitosa del perfil.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }


/// <summary>
/// Configures the menu for a specific profile.
/// </summary>
/// <param name="perfil">
/// The profile for which the menu configuration is to be updated.
/// </param>
/// <returns>
/// A boolean indicating whether the menu configuration was successful.
/// </returns>
        [HttpPut("ConfigurarMenu_Perfil")]
        public IActionResult ConfigurarMenu_Perfil([FromBody] Perfil perfil)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                perfil.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _perfilB.ConfigurarMenu_Perfil(perfil);
                webResult.Message = "Actualización exitosa del perfil.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }


        
    }
}
