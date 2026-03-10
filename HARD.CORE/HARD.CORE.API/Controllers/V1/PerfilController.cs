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
    /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for managing user profiles.
    /// </remarks>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    [ApiController]
    public class PerfilController : BaseController
    {

        private readonly IPerfilB _perfilB;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerfilController"/> class.
        /// </summary>
        /// <param name="perfilB">
        /// The profile business logic layer.
        /// </param>
        public PerfilController(IPerfilB perfilB)
        {
            _perfilB = perfilB;
        }

        /// <summary>
        /// Obtains a profile by its unique key.
        /// </summary>
        /// <param name="idPerfil">The unique key identifying the profile.</param>
        /// <returns>The profile associated with the provided key.</returns>
        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery, Required] int idPerfil)
        {
            var webResult = new WebResultModel<Perfil>();
            try
            {
                webResult.Data = (Perfil)_perfilB.GetById(idPerfil);
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
        /// Obtains all profiles.
        /// </summary>
        /// <returns>A list of all profiles.</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] bool? activo = null)
        {
            var webResult = new WebResultModel<List<Perfil>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    Filters = new BaseFilter()
                };
                webResult.Data = _perfilB.GetAll(pagedFilter).ToList();
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
        /// Obtains all profiles assigned to a specific user.
        /// </summary>
        /// <param name="idUsuario">The unique key identifying the user.</param>
        /// <returns>A list of profiles assigned to the specified user.</returns>
        [HttpGet("GetUserProfiles")]
        public IActionResult GetUserProfiles([FromQuery, Required] int idUsuario)
        {
            var webResult = new WebResultModel<List<Perfil>>();
            try
            {
                webResult.Data = _perfilB.GetUserProfiles(idUsuario: idUsuario);
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
        /// Inserts a new profile.
        /// </summary>
        /// <param name="perfil">The profile to insert.</param>
        /// <returns>The unique key of the inserted profile.</returns>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Perfil perfil)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                perfil.IdUsuarioCreacion = IdUsuario;
                perfil.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _perfilB.Add(perfil);
                webResult.Message = "Inserción exitosa del perfil.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al insertar perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        /// <summary>
        /// Updates an existing profile.
        /// </summary>
        /// <param name="perfil">The profile to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Perfil perfil)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                perfil.IdUsuarioModificacion = IdUsuario;
                webResult.Data = _perfilB.Update(perfil);
                webResult.Message = "Actualización exitosa del perfil.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al actualizar el perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

    }
}
