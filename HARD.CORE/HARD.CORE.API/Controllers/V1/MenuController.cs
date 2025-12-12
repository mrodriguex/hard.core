using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Helpers;
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
    public class MenuController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IMenuB _menuB;

        public MenuController(IConfiguration config, IMenuB menuB)
        {
            _config = config;
            _menuB = menuB;
        }

        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos([FromQuery] bool? claveEstatus = null)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                webResult.Data = _menuB.ObtenerTodos(claveEstatus: claveEstatus);
                webResult.Message = "Información del menú del usuario obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el menú del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);

        }

        // Obtener menú de usuario
        [HttpGet("ObtenerMenu_Usuario")]
        public IActionResult ObtenerMenu_Usuario([FromQuery, Required] string claveUsuario, [FromQuery, Required] int clavePerfil)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                string decodedClaveUsuario = Uri.UnescapeDataString(claveUsuario);
                webResult.Data = _menuB.ObtenerMenu_Usuario(claveUsuario: decodedClaveUsuario, clavePerfil: clavePerfil);
                webResult.Message = "Información del menú del usuario obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el menú del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        // Obtener menú de perfil
        [HttpGet("ObtenerMenu_Perfil")]
        public IActionResult ObtenerMenu_Perfil([FromQuery, Required] int clavePerfil)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                webResult.Data = _menuB.ObtenerMenu_Perfil(clavePerfil);
                webResult.Message = "Información del menú del perfil obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el menú del perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        // Configurar menú de perfil
        [HttpPost("ConfigurarMenu_Perfil")]
        public IActionResult ConfigurarMenu_Perfil([FromBody] Perfil perfil)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _menuB.ConfigurarMenu_Perfil(perfil.ClavePerfil, perfil.Menus);
                webResult.Message = "Configuración del menú por perfil realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la configuración del menú por perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
    }
}
