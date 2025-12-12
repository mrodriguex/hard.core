using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HARD.CORE.API.Controllers.V1
{
    /// <summary>
    /// Controller for managing motivo vacante. 
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for CRUD operations on motivo vacante.
    /// </remarks>    
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class MotivoVacanteController : BaseController
    {
        private IMotivoVacanteB _motivoVacanteB;
        public MotivoVacanteController(IMotivoVacanteB motivoVacanteB)
        {
            _motivoVacanteB = motivoVacanteB;
        }

        /// <summary>
        /// Gets a motivo vacante by its clave.
        /// </summary>
        /// <param name="claveMotivoVacante">
        /// The unique identifier of the motivo vacante.
        /// </param>
        /// <returns>
        /// A WebResultModel containing the motivo vacante data.
        /// </returns>
        [HttpGet("Obtener")]
        public IActionResult ObtenerTodos([FromQuery] int? claveMotivoVacante = null)
        {
            var webResult = new WebResultModel<MotivoVacante>();
            try
            {
                int claveMotivoVacanteValue = claveMotivoVacante.HasValue ? claveMotivoVacante.Value : 0;
                MotivoVacante motivoVacante = _motivoVacanteB.ObtenerTodos().FirstOrDefault(m => m.ClaveMotivoVacante == claveMotivoVacanteValue) ?? new MotivoVacante();
                webResult.Data = motivoVacante;
                webResult.Message = "Información de motivo vacante obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información del motivo de la vacante.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Gets all motivo vacante.
        /// </summary>
        /// <returns>A WebResultModel containing a list of motivo vacante.</returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos()
        {
            var webResult = new WebResultModel<List<MotivoVacante>>();
            try
            {
                webResult.Data = _motivoVacanteB.ObtenerTodos();
                webResult.Message = "Información de motivo vacante obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información del motivo de la vacante.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates a motivo vacante.
        /// </summary>
        /// <param name="motivoVacante">
        /// The motivo vacante object to update.
        /// </param>
        /// <returns>A WebResultModel indicating the success of the update operation.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] MotivoVacante motivoVacante)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                motivoVacante.ClaveUsuarioUltimaActualizacion = ClaveUsuario;
                webResult.Data = _motivoVacanteB.Actualizar(motivoVacante);
                webResult.Message = "Actualización exitosa del motivo de la vacante.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el motivo de la vacante.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
    }
}
