using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.NEG.Services;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace HARD.CORE.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class ClienteController : BaseController
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] bool? activo = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = _clienteService.GetAll(activo, pageIndex, pageSize);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery, Required] int idCliente)
        {
            var webResult = _clienteService.GetById(idCliente);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Cliente cliente)
        {
            var webResult = _clienteService.Add(cliente, IdUsuarioAutenticado);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Cliente cliente)
        {
            var webResult = _clienteService.Update(cliente, IdUsuarioAutenticado);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery, Required] int idCliente)
        {
            var webResult = _clienteService.Delete(idCliente, IdUsuarioAutenticado);
            return webResult.Success ? Ok(webResult) : BadRequest(webResult);
        }
    }
}
