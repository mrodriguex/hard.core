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
        /// <param name="claveEmpresa">The unique key identifying the company.</param>
        /// <returns>The company associated with the provided key.</returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int claveEmpresa)
        {
            var webResult = new WebResultModel<Empresa>();
            try
            {
                webResult.Data = (Empresa)_empresaB.Obtener(claveEmpresa);
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
        /// <param name="clavePerfil">
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
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos([FromQuery] int? clavePerfil = null, [FromQuery] string? claveUsuario = null)
        {
            var webResult = new WebResultModel<List<Empresa>>();
            try
            {
                webResult.Data = _empresaB.ObtenerTodos(clavePerfil: clavePerfil, claveUsuario: claveUsuario);
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
        /// <param name="clavePerfil">
        /// The unique key identifying the profile.
        /// </param>
        /// <param name="claveUsuario">
        /// The unique key identifying the user.
        /// </param>
        /// <returns>
        /// A list of companies assigned to the user.
        /// </returns>
        [HttpGet("ObtenerAsignado")]
        public IActionResult ObtenerAsignado([FromQuery, Required] int clavePerfil, [FromQuery, Required] string claveUsuario)
        {
            var webResult = new WebResultModel<List<Empresa>>();
            try
            {
                webResult.Data = _empresaB.ObtenerEmpresasUsuario(claveUsuario: claveUsuario);
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
