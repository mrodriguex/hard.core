using System;
using System.Collections.Generic;
using System.Linq;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.NEG.Services
{
    public class PerfilService
    {
        private readonly IPerfilB _perfilB;
        private readonly ILogger<PerfilService> _logger;
        private readonly IConfiguration _config;

        public PerfilService(ILogger<PerfilService> logger, IPerfilB perfilB, IConfiguration config)
        {
            _perfilB = perfilB;
            _logger = logger;
            _config = config;
        }

        public WebResultModel<Perfil> GetById(int idPerfil)
        {
            var webResult = new WebResultModel<Perfil>();
            try
            {
                webResult.Data = _perfilB.GetById(idPerfil);
                webResult.Message = "Información del perfil obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información del perfil con ID: {IdPerfil}", idPerfil);
                webResult.Message = "Error al obtener la información del perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
        public WebResultModel<List<Perfil>> GetAll(bool? activo = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<List<Perfil>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    PageIndex = pageIndex ?? 1,
                    PageSize = pageSize ?? int.MaxValue,
                    Filters = new BaseFilter
                    {
                        Activo = activo
                    }
                };
                webResult.Data = _perfilB.GetAll(pagedFilter).ToList();
                webResult.Message = "Información de los perfiles obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los perfiles");
                webResult.Message = "Error al obtener la información de los perfiles.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Add(Perfil perfil, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                perfil.IdUsuarioCreacion = idUsuarioAuenticado;
                perfil.IdUsuarioModificacion = idUsuarioAuenticado;
                perfil.FechaCreacion = DateTime.UtcNow;
                perfil.FechaModificacion = DateTime.UtcNow;
                _perfilB.Add(perfil);
                webResult.Data = true;
                webResult.Message = "Perfil agregado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el perfil");
                webResult.Message = "Error al agregar el perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Update(Perfil perfil, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                perfil.IdUsuarioModificacion = idUsuarioAuenticado;
                perfil.FechaModificacion = DateTime.UtcNow;
                _perfilB.Update(perfil);
                webResult.Data = true;
                webResult.Message = "Perfil actualizado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el perfil con ID: {IdPerfil}", perfil.Id);
                webResult.Message = "Error al actualizar el perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Delete(int idPerfil, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {                
                webResult.Data = _perfilB.Delete(idPerfil);
                webResult.Message = "Perfil eliminado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el perfil con ID: {IdPerfil}", idPerfil);
                webResult.Message = "Error al eliminar el perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<List<Perfil>> GetUserProfiles(int idUsuario)
        {
            var webResult = new WebResultModel<List<Perfil>>();
            try
            {
                webResult.Data = _perfilB.GetUserProfiles(idUsuario: idUsuario);
                webResult.Message = "Información de los perfiles del usuario obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los perfiles del usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al obtener la información de los perfiles del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

    }
}