using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
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
        private readonly IClienteB _clienteB;

        public ClienteController(IClienteB clienteB)
        {
            _clienteB = clienteB;
        }
        [HttpGet("GetAll")]

        public IActionResult GetAll([FromQuery] bool? activo = null)
        {
            var webResult = new WebResultModel<List<Cliente>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Filters = new BaseFilter { Activo = activo }
                };
                webResult.Data = _clienteB.GetAll(pagedFilter).ToList();
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

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery, Required] int idCliente)
        {
            var webResult = new WebResultModel<Cliente>();
            try
            {
                webResult.Data = _clienteB.GetById(idCliente);
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

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Cliente cliente)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                cliente.IdUsuarioCreacion = IdUsuario;
                webResult.Data = _clienteB.Add(cliente);
                webResult.Message = "Cliente agregado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al agregar el cliente.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Cliente cliente)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                cliente.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _clienteB.Update(cliente);
                webResult.Message = "Cliente actualizado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el cliente.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
