using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.NEG.Services;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace HARD.CORE.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    [ApiController]
    public class MenuController : BaseController
    {
        private readonly MenuService _menuService;

        public MenuController(IConfiguration config, MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery, Required] int idMenu)
        {
            var webResult = _menuService.GetById(idMenu);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] bool? activo = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = _menuService.GetAll(activo, pageIndex, pageSize);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        /// <summary>
        /// Inserts a new menu.
        /// </summary>
        /// <param name="menu">The menu to insert.</param>
        /// <returns>The unique key of the inserted menu.</returns>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Menu menu)
        {
            var webResult = _menuService.Add(menu, IdUsuarioAutenticado);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        /// <summary>
        /// Updates an existing menu.
        /// </summary>
        /// <param name="menu">The menu to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Menu menu)
        {
            var webResult = _menuService.Update(menu, IdUsuarioAutenticado);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery, Required] int idMenu)
        {
            var webResult = _menuService.Delete(idMenu, IdUsuarioAutenticado);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        // Obtener menú de usuario
        [HttpGet("GetMenusByUser")]
        public IActionResult GetMenusByUser([FromQuery, Required] int idUsuario, [FromQuery, Required] int idPerfil)
        {
            var webResult = _menuService.GetMenusByUser(idUsuario, idPerfil);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        // Obtener menú de perfil
        [HttpGet("GetMenusByProfile")]
        public IActionResult GetMenusByProfile([FromQuery, Required] int idPerfil)
        {
            var webResult = _menuService.GetMenusByProfile(idPerfil);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

    }
}
