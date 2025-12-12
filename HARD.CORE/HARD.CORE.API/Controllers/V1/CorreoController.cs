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
    public class CorreoController : BaseController
    {

        private ICorreoB _correoB;
        private ITipoCorreoB _tipoCorreoB;
        private ICorreoVariableB _correoVariableB;
        private IUsuarioB _usuarioB;

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
        public CorreoController(IUsuarioB usuarioB, ICorreoB correoB, ITipoCorreoB tipoCorreoB, ICorreoVariableB correoVariableB)
        {
            _usuarioB = usuarioB;
            _correoB = correoB;
            _tipoCorreoB = tipoCorreoB;
            _correoVariableB = correoVariableB;
        }

        /// <summary>
        /// Obtains an correo by its unique key.
        /// </summary>
        /// <param name="claveCorreo">
        /// Unique key of the correo to retrieve.
        /// </param>
        /// <returns>   
        /// The correo associated with the provided key.
        /// </returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int claveCorreo)
        {
            var webResult = new WebResultModel<Correo>();
            try
            {
                webResult.Data = _correoB.Obtener(claveCorreo) ?? new Correo();
                webResult.Message = "Información del correo con clave " + claveCorreo.ToString() + " obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información del correo.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
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
            var webResult = new WebResultModel<List<Correo>>();
            try
            {
                webResult.Data = _correoB.ObtenerTodos(estatus: estatus);
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
        [HttpGet("ObtenerActivosLista")]
        public IActionResult ObtenerActivosLista()
        {
            var webResult = new WebResultModel<List<Correo>>();
            try
            {
                webResult.Data = _correoB.ObtenerTodos();
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
        /// Obtiene un listado de los tipos de correo.
        /// </summary>
        /// <param name="estatus">
        /// Estatus para filtrar los tipos de correo (opcional).
        /// </param>
        /// <returns>
        /// Obtiene un listado de los tipos
        /// </returns>
        [HttpGet("ObtenerTiposCorreo")]
        public IActionResult ObtenerTiposCorreo([FromQuery] bool? estatus = null)
        {
            var webResult = new WebResultModel<List<TipoCorreo>>();

            try
            {
                webResult.Data = _tipoCorreoB.ObtenerTodos(estatus: estatus);
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
        /// Obtiene un listado de los tipos de correo.
        /// </summary>
        /// <param name="estatus">
        /// Estatus para filtrar los tipos de correo (opcional).
        /// </param>
        /// <returns>
        /// Obtiene un listado de los tipos
        /// </returns>
        [HttpGet("ObtenerCorreosUsuarios")]
        public IActionResult ObtenerCorreosUsuarios([FromQuery] bool? estatus = null)
        {
            var webResult = new WebResultModel<List<string>>();

            try
            {
                webResult.Data = _usuarioB.ObtenerTodos().Select(u => u.Correo).ToList();
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
        /// Obtiene un listado de las variables de correo.
        /// </summary>
        /// <param name="claveTipoCorreo">
        /// Clave del tipo de correo.
        /// </param>
        /// <returns>
        /// Obtiene un listado de las variables de correo.
        /// </returns>
        [HttpGet("ObtenerVariables")]
        public IActionResult ObtenerVariables([FromQuery] int? claveTipoCorreo = null)
        {
            var webResult = new WebResultModel<List<CorreoVariable>>();

            try
            {
                webResult.Data = _correoVariableB.ObtenerTodos(claveTipoCorreo: claveTipoCorreo, estatus: true);
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
        public IActionResult Insertar([FromBody] Correo correo)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                correo.ClaveUsuarioActualizacion = ClaveUsuario;
                webResult.Data = _correoB.Insertar(correo);
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
        public IActionResult Actualizar([FromBody] Correo correo)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                correo.ClaveUsuarioActualizacion = ClaveUsuario;
                webResult.Data = _correoB.Actualizar(correo);
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
