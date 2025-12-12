using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Helpers;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HARD.CORE.API.Controllers.V1
{

    /// <summary>
    /// Controller for managing avisos. 
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for CRUD operations on avisos.
    /// </remarks>    
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class NivelMinimoEstudiosController : BaseController
    {
        private INivelMinimoEstudiosB _nivelMinimoEstudiosB;

        /// <summary>
        /// Initializes a new instance of the <see cref="NivelMinimoEstudiosController"/> class.
        /// </summary>
        /// <param name="inglesB">
        /// The ingles business logic layer instance.
        /// </param>
        /// <remarks>   
        /// This constructor initializes the controller with the provided business logic layer instance.
        /// </remarks>
        public NivelMinimoEstudiosController(INivelMinimoEstudiosB nivelMinimoEstudiosB)
        {
            _nivelMinimoEstudiosB = nivelMinimoEstudiosB;
        }

        /// <summary>
        /// Obtains an ingles by its unique key.
        /// </summary>
        /// <param name="claveNivelMinimoEstudios">
        /// Unique key of the ingles to retrieve.
        /// </param>
        /// <returns>
        /// The ingles associated with the provided key.
        /// </returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int claveNivelMinimoEstudios)
        {
            var webResult = new WebResultModel<NivelMinimoEstudios>();
            try
            {
                webResult.Data = _nivelMinimoEstudiosB.Obtener(claveNivelMinimoEstudios: claveNivelMinimoEstudios);
                webResult.Message = "Información del nivel mínimo de estudios con clave " + claveNivelMinimoEstudios.ToString() + " obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información del nivel de inglés.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Obtains all ingleses.
        /// </summary>
        /// <param name="estatus">
        /// The status to filter ingleses.
        /// </param>
        /// <returns>
        /// A list of all ingleses.
        /// </returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos([FromQuery] bool? estatus = null)
        {
            var webResult = new WebResultModel<List<NivelMinimoEstudios>>();
            try
            {
                webResult.Data = _nivelMinimoEstudiosB.ObtenerTodos(estatus: estatus);
                webResult.Message = "Información de niveles mínimos de estudios obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el listado de niveles mínimos de estudios.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Inserts a new nivel mínimo de estudios into the system.
        /// </summary>
        /// <param name="nivelMinimoEstudios">
        /// The nivel mínimo de estudios to insert.
        /// </param>
        /// <returns>
        /// The unique key of the inserted nivel mínimo de estudios.
        /// </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] NivelMinimoEstudios nivelMinimoEstudios)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                nivelMinimoEstudios.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _nivelMinimoEstudiosB.Insertar(nivelMinimoEstudios: nivelMinimoEstudios);
                webResult.Message = "Inserción exitosa del nivel mínimo de estudios.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al insertar el nivel de inglés.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates an existing ingles in the system.
        /// </summary>
        /// <param name="ingles">
        /// The ingles to update.
        /// </param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] NivelMinimoEstudios nivelMinimoEstudios)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                nivelMinimoEstudios.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _nivelMinimoEstudiosB.Actualizar(nivelMinimoEstudios: nivelMinimoEstudios);
                webResult.Message = "Actualización exitosa del nivel mínimo de estudios.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el nivel mínimo de estudios.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
