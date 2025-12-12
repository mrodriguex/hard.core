using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG;
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
    public class ClienteController : BaseController
    {
        [HttpGet("ObtenerTodos")]
        
        public IActionResult ObtenerTodos()
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = ClienteB.GetInstance().ObtenerTodos();
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

        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int claveCliente)
        {
            var webResult = new WebResultModel<Cliente>();
            try
            {
                webResult.Data = (Cliente)ClienteB.GetInstance().Obtener(claveCliente);
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
        [HttpGet("ObtenerActivos")]
        public IActionResult ObtenerActivos()
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = ClienteB.GetInstance().ObtenerActivos();
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

        [HttpGet("ObtenerNoRegistrados")]
        public IActionResult ObtenerNoRegistrados()
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = ClienteB.GetInstance().ObtenerNoRegistrados();
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
