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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class SeguridadAccionController : BaseController
    {
        private ISeguridadAccionB _seguridadAccionB;

        public SeguridadAccionController(ISeguridadAccionB seguridadAccionB)
        {
            _seguridadAccionB = seguridadAccionB;
        }
      
        [HttpGet("ObtenerPorPerfil")]
        public IActionResult ObtenerPorPerfil([FromQuery, Required] int clavePerfil)
        {
            var webResult = new WebResultModel<List<SeguridadAccion>>();

            try
            {
                webResult.Data = _seguridadAccionB.ObtenerTodos(clavePerfil);
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

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] List<SeguridadAccion> listadoSeguridadAccion)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                listadoSeguridadAccion.ForEach(sa => sa.ClaveUsuarioUltimaActualizacion = ClaveUsuario);
                
                webResult.Data = _seguridadAccionB.Actualizar(listadoSeguridadAccion);
                if (webResult.Data)
                {
                    webResult.Message = "Actualización exitosa de la seguridad de acción.";
                    webResult.Success = true;
                }
                else
                {
                    webResult.Message = "Ocurrió un error al actualizar la seguridad de acción.";
                }
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar la seguridad de acción.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
    }
}
