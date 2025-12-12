using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HARD.CORE.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    [ApiController]
    public class PagosController : BaseController
    {

        // Obtener reporte de cartera por clave de cliente
        [HttpGet("ObtenerReporteCartera")]
        public IActionResult ObtenerReporteCartera([FromQuery, Required] int claveCliente)
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = PagosB.GetInstance().ObtenerReporteCartera(claveCliente);
                webResult.Message = "Información obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al cargar la información.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
    }
}
