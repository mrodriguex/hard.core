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
    /// Controller for managing correos. 
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for CRUD operations on correos.
    /// </remarks>    
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class HerenciaPerfilController : BaseController
    {

        private IHerenciaPerfilB _herenciaPerfilB;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorreoController"/> class.
        /// </summary>
        /// <param name="correoB">
        /// The correo business logic layer instance.
        /// </param>    
        /// <param name="tipoCorreoB">
        /// The tipo correo business logic layer instance.
        /// </param>
        /// <param name="correoVariableB">
        /// The correo variable business logic layer instance.
        /// </param>
        public HerenciaPerfilController(IHerenciaPerfilB herenciaPerfilB)
        {
            _herenciaPerfilB = herenciaPerfilB;
        }

        /// <summary>
        /// Obtiene un listado de los correos.
        /// </summary>
        /// <param name="estatus">
        /// Estatus para filtrar los correos (opcional).
        /// </param>
        /// <returns>
        /// Obtiene un listado de los correos 
        /// </returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos([FromQuery] bool? estatus = null)
        {
            var webResult = new WebResultModel<List<HerenciaPerfil>>();
            try
            {
                webResult.Data = _herenciaPerfilB.ObtenerTodos(estatus: estatus);
                webResult.Message = "Información de correos obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el listado de correos.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Obtiene un listado de los correos activos
        /// </summary>
        /// <returns>
        /// Obtiene un listado de los correos 
        /// </returns>
        [HttpPut("Existe")]
        public IActionResult Existe(HerenciaPerfil herenciaPerfil)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _herenciaPerfilB.Existe(herenciaPerfil);
                webResult.Message = "Información de correos obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el listado de correos.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Inserts a new correo into the system.
        /// </summary>
        /// <param name="correo">
        /// The correo to insert.
        /// </param>
        /// <returns>
        /// The unique key of the inserted correo.
        /// </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] HerenciaPerfil herenciaPerfil)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                webResult.Data = _herenciaPerfilB.Insertar(herenciaPerfil);
                webResult.Message = "Inserción exitosa del correo.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al insertar el correo.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates an existing correo in the system.
        /// </summary>
        /// <param name="correo">The correo to update.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] HerenciaPerfil herenciaPerfil)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _herenciaPerfilB.Actualizar(herenciaPerfil);
                webResult.Message = "Actualización exitosa del correo.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el correo.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
