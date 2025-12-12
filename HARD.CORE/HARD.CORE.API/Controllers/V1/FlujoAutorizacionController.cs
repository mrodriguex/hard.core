using Asp.Versioning;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public class FlujoAutorizacionController : ControllerBase
    {
        private IFlujoAutorizacionB _flujoAutorizacionB;
        public FlujoAutorizacionController(IFlujoAutorizacionB flujoAutorizacionB)
        {
            _flujoAutorizacionB = flujoAutorizacionB;
        }
        /// <summary>
        /// Obtains all avisos.
        /// </summary>
        /// <returns>
        /// A list of all avisos.
        /// </returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos()
        {
            var webResult = new WebResultModel<List<FlujoAutorizacion>>();
            try
            {
                webResult.Data = _flujoAutorizacionB.ObtenerTodos();
                webResult.Message = "Información de flujo de autorización obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el listado de flujo de autorización";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
    }
}
