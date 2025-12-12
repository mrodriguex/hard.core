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
    public class NivelInglesController : BaseController
    {
        private INivelInglesB _nivelInglesB;
   
        /// <summary>
        /// Initializes a new instance of the <see cref="NivelInglesController"/> class.
        /// </summary>
        /// <param name="nivelInglesB">
        /// The nivelIngles business logic layer instance.
        /// </param>
        /// <remarks>
        /// This constructor initializes the controller with the provided business logic layer instance.
        /// </remarks>
        public NivelInglesController(INivelInglesB nivelInglesB)
        {
            _nivelInglesB = nivelInglesB;
        }

        /// <summary>
        /// Obtains an nivelIngles by its unique key.
        /// </summary>
        /// <param name="claveNivelIngles">
        /// Unique key of the nivelIngles to retrieve.
        /// </param>
        /// <returns>
        /// The nivelIngles associated with the provided key.
        /// </returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int claveNivelIngles)
        {
            var webResult = new WebResultModel<NivelIngles>();
            try
            {
                webResult.Data = _nivelInglesB.Obtener(claveNivelIngles: claveNivelIngles);
                webResult.Message = "Información del nivel de inglés con clave " + claveNivelIngles.ToString() + " obtenida correctamente.";
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
        /// Obtains all nivelIngleses.
        /// </summary>
        /// <param name="estatus">
        /// The status to filter nivelIngleses.
        /// </param>
        /// <returns>
        /// A list of all nivelIngleses.
        /// </returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos([FromQuery] bool? estatus = null)
        {
            var webResult = new WebResultModel<List<NivelIngles>>();
            try
            {
                webResult.Data = _nivelInglesB.ObtenerTodos(estatus: estatus);
                webResult.Message = "Información de niveles de inglés obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el listado de niveles de inglés.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Inserts a new nivelIngles into the system.
        /// </summary>
        /// <param name="nivelIngles">
        /// The nivelIngles to insert.
        /// </param>
        /// <returns>
        /// The unique key of the inserted nivelIngles.
        /// </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] NivelIngles nivelIngles)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                nivelIngles.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _nivelInglesB.Insertar(nivelIngles);
                webResult.Message = "Inserción exitosa del nivel de inglés.";
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
        /// Updates an existing nivelIngles in the system.
        /// </summary>
        /// <param name="nivelIngles">
        /// The nivelIngles to update.
        /// </param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] NivelIngles nivelIngles)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                nivelIngles.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _nivelInglesB.Actualizar(nivelIngles);
                webResult.Message = "Actualización exitosa del nivel de inglés.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el nivel de inglés.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
