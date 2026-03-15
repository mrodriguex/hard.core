using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.DAT.Repositories
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

        public async Task<Menu> GetByIdAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            return menu;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync(PagedFilter<BaseFilter> pagedFilter)
        {
            var menus = await _context.Menus
                .Where(m => (!pagedFilter.Filters.Activo.HasValue || m.Activo == pagedFilter.Filters.Activo.Value)
                    && (string.IsNullOrEmpty(pagedFilter.Filters.Nombre) || m.Nombre.Contains(pagedFilter.Filters.Nombre)))
                .OrderBy(m => m.Id)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToListAsync();

            return menus.AsEnumerable();
        }

        public async Task<int> AddAsync(Menu entity)
        {
            _context.Menus.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return false;
            }
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Menu entity)
        {
            _context.Menus.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
