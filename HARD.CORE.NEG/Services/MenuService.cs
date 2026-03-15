using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.NEG.Services
{
    public class MenuService : IMenuService
    {
        private readonly IRepositoryBase<Menu, BaseFilter, int> _menuRepository;
        private readonly IRepositoryBase<Usuario, BaseFilter, int> _usuarioRepository;
        private readonly IRepositoryBase<Perfil, BaseFilter, int> _perfilRepository;
        private readonly ILogger<MenuService> _logger;        

        public MenuService(ILogger<MenuService> logger,
        IRepositoryBase<Menu, BaseFilter, int> menuRepository,
        IRepositoryBase<Usuario, BaseFilter, int> usuarioRepository,
        IRepositoryBase<Perfil, BaseFilter, int> perfilRepository)
        {
            _menuRepository = menuRepository;
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _logger = logger;
        }

        #region Implementation of IServiceBase
        public async Task<WebResultModel<Menu>> GetByIdAsync(int idMenu)
        {
            var webResult = new WebResultModel<Menu>();
            try
            {
                webResult.Data = await _menuRepository.GetByIdAsync(idMenu);
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

        public async Task<WebResultModel<IEnumerable<Menu>>> GetAllAsync(PagedFilter<BaseFilter> pagedFilter)
        {
            var webResult = new WebResultModel<IEnumerable<Menu>>();
            try
            {
                webResult.Data = (await _menuRepository.GetAllAsync(pagedFilter)).ToList();
                webResult.Message = "Información obtenida exitosamente.";
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

        public async Task<WebResultModel<int>> AddAsync(Menu menu, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                menu.IdUsuarioCreacion = idUsuarioAuenticado;
                menu.IdUsuarioModificacion = idUsuarioAuenticado;
                menu.FechaCreacion = DateTime.UtcNow;
                menu.FechaModificacion = DateTime.UtcNow;
                webResult.Data = await _menuRepository.AddAsync(menu);
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

        public async Task<WebResultModel<bool>> UpdateAsync(Menu menu, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                menu.IdUsuarioModificacion = idUsuarioAuenticado;
                menu.FechaModificacion = DateTime.UtcNow;

                webResult.Data = await _menuRepository.UpdateAsync(menu);
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

        public async Task<WebResultModel<bool>> DeleteAsync(int idMenu, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = await _menuRepository.DeleteAsync(idMenu);
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
        #endregion

        #region Implementation of IMenuService
        public async Task<WebResultModel<IEnumerable<Menu>>> GetAllAsync(bool? activo = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<IEnumerable<Menu>>();
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
                webResult = await GetAllAsync(pagedFilter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los menues");
                webResult.Message = "Error al obtener la información de los menues.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<IEnumerable<Menu>>> GetMenusByUserAsync(int idUsuario, int idPerfil)
        {
            var webResult = new WebResultModel<IEnumerable<Menu>>();
            try
            {
                Usuario usuario = await _usuarioRepository.GetByIdAsync(idUsuario);
                List<Menu> menus = usuario.Perfiles.Where(p => p.Id == idPerfil)
                    .SelectMany(p => p.Menus)
                    .ToList();
                webResult.Data = menus ?? new List<Menu>();
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

        public async Task<WebResultModel<IEnumerable<Menu>>> GetMenusByProfileAsync(int idPerfil)
        {
            var webResult = new WebResultModel<IEnumerable<Menu>>();
            try
            {
                Perfil perfil = await _perfilRepository.GetByIdAsync(idPerfil);
                List<Menu> menus = perfil.Menus.ToList();
                webResult.Data = menus ?? new List<Menu>();
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

        #endregion

    }
}