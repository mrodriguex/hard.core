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
    public class NotificacionController : BaseController
    {
        private INotificacionB _notificacionB;

        public NotificacionController(INotificacionB notificacionB)
        {
            _notificacionB = notificacionB;
        }

        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int claveNotificacion)
        {
            var webResult = new WebResultModel<Notificacion>();
            try
            {
                webResult.Data = _notificacionB.Obtener(claveNotificacion);
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
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos()
        {
            var webResult = new WebResultModel<List<Notificacion>>();
            try
            {
                webResult.Data = _notificacionB.ObtenerTodos();
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

        [HttpGet("ObtenerUsuariosPermitidos")]
        public IActionResult ObtenerUsuariosPermitidos([FromQuery] int? claveNotificacion = null)
        {
            var webResult = new WebResultModel<List<Notificacion>>();
            try
            {
                webResult.Data = _notificacionB.ObtenerUsuariosPermitidos(claveNotificacion);
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
        [HttpGet("ObtenerPorUsuario")]
        public IActionResult ObtenerPorUsuario([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<List<Notificacion>>();
            try
            {
                webResult.Data = (List<Notificacion>)_notificacionB.ObtenerPorUsuario(claveUsuario).Select(x => (Notificacion)x);
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
        [HttpGet("ExistenPendientesPorLeer")]
        public IActionResult ExistenPendientesPorLeer([FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _notificacionB.ExistenPendientesPorLeer(claveUsuario);
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
        [HttpGet("ConClientesEspecificos")]
        public IActionResult ConClientesEspecificos([FromQuery, Required] int claveNotificacion)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = false;
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
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] Notificacion notificacion)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                webResult.Data = _notificacionB.Insertar(notificacion);
                webResult.Message = "Inserción exitosa de la notificación.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al insertar la notificación.";
                webResult.Errors.Add(ex.Message);
            }

            return Ok(webResult);
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Notificacion notificacion)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _notificacionB.Actualizar(notificacion);
                webResult.Message = "Actualización exitosa de la notificación.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar la notificación.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
        [HttpPut("MarcarComoVisto")]
        public IActionResult MarcarComoVisto([FromBody] NotificacionDetalle notificacionDetalle)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _notificacionB.MarcarComoVisto(notificacionDetalle);
                webResult.Message = "La notificación se ha marcado como leída exitosamente.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al marcar como leída la notificación.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
    }
}
