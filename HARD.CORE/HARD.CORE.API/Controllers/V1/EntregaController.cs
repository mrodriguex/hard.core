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
    public class EntregaController : BaseController
    {

        [HttpGet("ObtenerClavesArrendamientos")]
        public IActionResult ObtenerClavesArrendamientos([FromQuery] int? claveCliente = null)
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = EntregaB.GetInstance().ObtenerClavesArrendamientos(claveCliente);
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
        [HttpGet("ObtenerPorCliente")]
        public IActionResult ObtenerPorCliente([FromQuery, Required] int claveCliente, [FromQuery, Required] DateTime fechaInicio, [FromQuery, Required] DateTime fechaFin, [FromQuery] int? claveProducto = null)
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                string decodedFechaInicio = Uri.UnescapeDataString(fechaInicio.ToString());
                string decodedFechaFin = Uri.UnescapeDataString(fechaFin.ToString());
                webResult.Data = EntregaB.GetInstance().ObtenerPorCliente(claveCliente, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), claveProducto);
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
        [HttpGet("ObtenerArrendamientosPorCliente")]
        public IActionResult ObtenerArrendamientosPorCliente([FromQuery] int claveCliente, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin, [FromQuery] string claveArrendamiento = "")
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                string decodedFechaInicio = Uri.UnescapeDataString(fechaInicio.ToString());
                string decodedFechaFin = Uri.UnescapeDataString(fechaFin.ToString());
                webResult.Data = EntregaB.GetInstance().ObtenerArrendamientosPorCliente(claveCliente, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), claveArrendamiento);
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
