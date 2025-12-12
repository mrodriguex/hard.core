using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    [ApiController]
    public class SugerenciaController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly ISugerenciaB _sugerenciaB;

        public SugerenciaController(IConfiguration config, ISugerenciaB sugerenciaB)
        {
            _config = config;
            _sugerenciaB = sugerenciaB;
        }


        // Obtener todas las sugerencias
        [HttpGet("ObtenerSugerencias")]
        public IActionResult ObtenerSugerencias()
        {
            var webResult = new WebResultModel<DataTable>();
            try
            {
                webResult.Data = _sugerenciaB.ObtenerSugerencias();
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
