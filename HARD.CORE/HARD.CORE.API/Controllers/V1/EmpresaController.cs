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
    /// <summary>
    /// Controller for managing user companies.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for managing user companies.
    /// </remarks>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    [ApiController]
    public class EmpresaController : BaseController
    {

        private readonly IEmpresaB _empresaB;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmpresaController"/> class.
        /// </summary>
        /// <param name="empresaB">
        /// The company business logic layer.
        /// </param>
        public EmpresaController(IEmpresaB empresaB)
        {
            _empresaB = empresaB;
        }

        /// <summary>
        /// Obtains a company by its unique key.
        /// </summary>
        /// <param name="idEmpresa">The unique key identifying the company.</param>
        /// <returns>The company associated with the provided key.</returns>
        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery, Required] int idEmpresa)
        {
            var webResult = new WebResultModel<Empresa>();
            try
            {
                webResult.Data = _empresaB.GetById(idEmpresa);
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

        /// <summary>
        /// Obtains all companies.
        /// </summary>
        /// <param name="idPerfil">
        /// The unique key identifying the profile.
        /// </param>
        /// <param name="claveUsuario">
        /// The unique key identifying the user.
        /// </param>
        /// <param name="estatus">
        /// The status to filter companies.
        /// </param>
        /// <returns>
        /// A list of companies matching the specified criteria.
        /// </returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] int? idPerfil = null, [FromQuery] int? idUsuario = null)
        {
            var webResult = new WebResultModel<List<Empresa>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    Filters = new BaseFilter
                    {
                        IdMaster = idUsuario,
                        IdDetail = idPerfil
                    }
                };
                webResult.Data = _empresaB.GetAll(pagedFilter).ToList();
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

        /// <summary>
        /// Obtains all companies assigned to a user.
        /// </summary>
        /// <param name="idUsuario">
        /// The unique key identifying the user.
        /// </param>
        /// <returns>
        /// A list of companies assigned to the user.
        /// </returns>
        [HttpGet("GetCompaniesByUser")]
        public IActionResult GetCompaniesByUser([FromQuery, Required] int idUsuario)
        {
            var webResult = new WebResultModel<List<Empresa>>();
            try
            {
                webResult.Data = _empresaB.GetCompaniesByUser(idUsuario: idUsuario);
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
        public IActionResult Add([FromBody] Empresa empresa)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                empresa.IdUsuarioCreacion = IdUsuario;
                webResult.Data = _empresaB.Add(empresa);
                webResult.Message = "Empresa agregada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al agregar la empresa.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Empresa empresa)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                empresa.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _empresaB.Update(empresa);
                webResult.Message = "Empresa actualizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar la empresa.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}