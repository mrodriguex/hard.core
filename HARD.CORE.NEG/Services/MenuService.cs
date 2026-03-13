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
    public class MenuService
    {
        private readonly IMenuB _menuB;
        private readonly ILogger<MenuService> _logger;
        private readonly IConfiguration _config;

        public MenuService(ILogger<MenuService> logger, IMenuB menuB, IConfiguration config)
        {
            _menuB = menuB;
            _logger = logger;
            _config = config;
        }

        public WebResultModel<Menu> GetById(int idMenu)
        {
            var webResult = new WebResultModel<Menu>();
            try
            {
                webResult.Data = _menuB.GetById(idMenu);
                webResult.Message = "Información del menu obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información del menu con ID: {IdMenu}", idMenu);
                webResult.Message = "Error al obtener la información del menu.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
        public WebResultModel<List<Menu>> GetAll(bool? activo = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<List<Menu>>();
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
                webResult.Data = _menuB.GetAll(pagedFilter).ToList();
                webResult.Message = "Información de los menues obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los menues");
                webResult.Message = "Error al obtener la información de los menues.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<int> Add(Menu menu, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                menu.IdUsuarioCreacion = idUsuarioAuenticado;
                menu.IdUsuarioModificacion = idUsuarioAuenticado;
                menu.FechaCreacion = DateTime.UtcNow;
                menu.FechaModificacion = DateTime.UtcNow;                
                webResult.Data = _menuB.Add(menu);
                webResult.Message = "Menu agregado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el menu");
                webResult.Message = "Error al agregar el menu.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Update(Menu menu, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                menu.IdUsuarioModificacion = idUsuarioAuenticado;
                menu.FechaModificacion = DateTime.UtcNow;
                _menuB.Update(menu);
                webResult.Data = true;
                webResult.Message = "Menu actualizado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el menu con ID: {IdMenu}", menu.Id);
                webResult.Message = "Error al actualizar el menu.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Delete(int idMenu, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _menuB.Delete(idMenu);
                webResult.Message = "Menu eliminado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el menu con ID: {IdMenu}", idMenu);
                webResult.Message = "Error al eliminar el menu.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<List<Menu>> GetMenusByUser(int idUsuario, int idPerfil)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                webResult.Data = _menuB.GetMenusByUser(idUsuario: idUsuario, idPerfil: idPerfil);
                webResult.Message = "Información de los menues del usuario obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los menues del usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al obtener la información de los menues del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<List<Menu>> GetMenusByProfile(int idPerfil)
        {
            var webResult = new WebResultModel<List<Menu>>();
            try
            {
                webResult.Data = _menuB.GetMenusByProfile(idPerfil: idPerfil);
                webResult.Message = "Información de los menues del perfil obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los menues del perfil con ID: {IdPerfil}", idPerfil);
                webResult.Message = "Error al obtener la información de los menues del perfil.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
    }
}