using System;
using System.Collections.Generic;
using System.Linq;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.DAT
{
    public class MenuDA : IRepositoryBase<Menu, BaseFilter, int>
    {

        private readonly HardCoreDbContext _context;
        private readonly ILogger<MenuDA> _logger;

        public MenuDA(HardCoreDbContext context, ILogger<MenuDA> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Menu GetById(int id)
        {
            var menu = _context.Menus.Find(id);
            return menu;
        }

        public IEnumerable<Menu> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            var menus = _context.Menus
                .Where(m => (!pagedFilter.Filters.Activo.HasValue || m.Activo == pagedFilter.Filters.Activo.Value)
                    && (string.IsNullOrEmpty(pagedFilter.Filters.Nombre) || m.Nombre.Contains(pagedFilter.Filters.Nombre)))
                .OrderBy(m => m.IdMenu)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToList();

            return menus.AsEnumerable();
        }

        public int Add(Menu entity)
        {
            try
            {
                _context.Menus.Add(entity);
                _context.SaveChanges();
                return entity.IdMenu;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el menú");
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var menu = _context.Menus.Find(id);
                if (menu == null)
                {
                    return false;
                }
                _context.Menus.Remove(menu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el menú");
                throw;
            }
        }

        public bool Update(Menu entity)
        {
            try{
                _context.Menus.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el menú");
                throw;
            }
        }

    }

}
