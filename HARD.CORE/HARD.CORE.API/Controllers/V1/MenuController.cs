using Asp.Versioning;
using Azure;
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

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery, Required] int idMenu)
        {
            var webResult = new WebResultModel<Menu>();
            try
            {
                webResult.Data = _menuB.GetById(idMenu);
                webResult.Message = "Información del menú obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información del menú.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] bool? activo = null)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Filters = new BaseFilter { Activo = activo }
                };
                webResult.Data = _menuB.GetAll(pagedFilter).ToList();
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
        [HttpGet("GetMenusByUser")]
        public IActionResult GetMenusByUser([FromQuery, Required] int idUsuario, [FromQuery, Required] int idPerfil)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                webResult.Data = _menuB.GetMenusByUser(idUsuario, idPerfil);
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
        [HttpGet("GetMenusByProfile")]
        public IActionResult GetMenusByProfile([FromQuery, Required] int idPerfil)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                webResult.Data = _menuB.GetMenusByProfile(idPerfil);
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

        /// <summary>
        /// Inserts a new menu.
        /// </summary>
        /// <param name="menu">The menu to insert.</param>
        /// <returns>The unique key of the inserted menu.</returns>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Menu menu)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                menu.IdUsuarioCreacion = IdUsuario;
                menu.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _menuB.Add(menu);
                webResult.Message = "Inserción exitosa del menú.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la inserción.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates an existing menu.
        /// </summary>
        /// <param name="menu">The menu to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Menu menu)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                menu.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _menuB.Update(menu);
                webResult.Message = "Actualización exitosa del menú.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el menú.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
