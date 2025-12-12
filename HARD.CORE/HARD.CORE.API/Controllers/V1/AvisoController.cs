using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Helpers;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HARD.CORE.API.Controllers.V1
{

    /// <summary>
    /// Controller for managing avisos. 
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for CRUD operations on avisos.
    /// </remarks>    
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class AvisoController : BaseController
    {
        private IAvisoB _avisoB;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvisoController"/> class.
        /// </summary>
        /// <param name="avisoB">
        /// The aviso business logic layer instance.
        /// </param>    
        public AvisoController(IAvisoB avisoB)
        {
            _avisoB = avisoB;
        }

        /// <summary>
        /// Obtains an aviso by its unique key.
        /// </summary>
        /// <param name="claveAviso">
        /// Unique key of the aviso to retrieve.
        /// </param>
        /// <returns>   
        /// The aviso associated with the provided key.
        /// /// </returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener([FromQuery, Required] int claveAviso)
        {
            var webResult = new WebResultModel<Aviso>();
            try
            {
                webResult.Data = _avisoB.Obtener(claveAviso);
                webResult.Message = "Información del aviso con clave " + claveAviso.ToString() + " obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información del aviso.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Obtains the photo of an aviso by its unique key.
        /// </summary>
        /// <param name="claveAviso">
        /// Unique key of the aviso to retrieve the photo.
        /// </param>
        /// <returns>
        /// The photo of the aviso associated with the provided key.
        /// </returns>  
        [HttpGet("ObtenerFoto")]
        public IActionResult ObtenerFoto([FromQuery, Required] int claveAviso)
        {
            var webResult = new WebResultModel<Aviso>();
            try
            {
                webResult.Data = _avisoB.Obtener(claveAviso);
                webResult.Message = "Información del aviso con clave " + claveAviso.ToString() + " obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener la información del aviso.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Obtains all avisos.
        /// </summary>
        /// <returns>
        /// A list of all avisos.
        /// </returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos()
        {
            var webResult = new WebResultModel<List<Aviso>>();
            try
            {
                webResult.Data = _avisoB.ObtenerTodos();
                webResult.Message = "Información de avisos obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el listado de avisos.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }
        [HttpGet("ObtenerActivosLista")]
        public IActionResult ObtenerActivosLista()
        {
            var webResult = new WebResultModel<List<Aviso>>();
            try
            {
                webResult.Data = _avisoB.ObtenerActivosLista();
                webResult.Message = "Información de avisos obtenida correctamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al obtener el listado de avisos.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Inserts a new aviso into the system.
        /// </summary>
        /// <param name="aviso">
        /// The aviso to insert.
        /// </param>
        /// <returns>
        /// The unique key of the inserted aviso.
        /// </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] Aviso aviso)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                aviso.ClaveUsuarioActualizacion = ClaveUsuario;
                webResult.Data = _avisoB.Insertar(aviso);
                webResult.Message = "Inserción exitosa del aviso.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al insertar el aviso.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates an existing aviso in the system.
        /// </summary>
        /// <param name="aviso">The aviso to update.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Aviso aviso)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                aviso.ClaveUsuarioActualizacion = ClaveUsuario;
                webResult.Data = _avisoB.Actualizar(aviso);
                webResult.Message = "Actualización exitosa del aviso.";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el aviso.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Deletes an existing aviso from the system.
        /// </summary>
        /// <param name="aviso">The aviso to delete.</param>
        /// <returns>The result of the delete operation.</returns> 
        [HttpPut("Eliminar")]
        public IActionResult Eliminar([FromBody] int ClaveAviso)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _avisoB.Eliminar(ClaveAviso, ClaveUsuario);
                webResult.Message = "El aviso ha sido eliminado con éxito";
                webResult.Success = true;

            }
            catch (Exception ex)
            {
                webResult.Message = "Error al eliminar el aviso.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
