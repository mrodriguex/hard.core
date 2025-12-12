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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class PrecioController : BaseController
    {
        [HttpGet("ObtenerPorClienteProducto")]
        public IActionResult ObtenerPorClienteProducto([FromQuery, Required] int claveCliente, [FromQuery, Required] int claveProducto)
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = PrecioB.GetInstance().ObtenerPorClienteProducto(claveCliente, claveProducto);
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
        [HttpGet("ObtenerDatosContrato")]
        public IActionResult ObtenerDatosContrato([FromQuery, Required] int claveCliente, [FromQuery, Required] int claveProducto)
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = PrecioB.GetInstance().ObtenerDatosContrato(claveCliente, claveProducto);
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
    }
}
