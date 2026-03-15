using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Repositories
{
    public class EmpresaDA : IRepositoryBase<Empresa, BaseFilter, int>
    {

        private readonly HardCoreDbContext _context;
        private readonly ILogger<EmpresaDA> _logger;

        public EmpresaDA(HardCoreDbContext context, ILogger<EmpresaDA> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Private


        #endregion

        public async Task<Empresa> GetByIdAsync(int id)
        {
            var empresa = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);
            return empresa;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync(PagedFilter<BaseFilter> pagedFilter)
        {
            var query = _context.Empresas.AsQueryable();
            if (pagedFilter.Filters?.Activo.HasValue == true)
            {
                query = query.Where(e => e.Activo == pagedFilter.Filters.Activo.Value);
            }
            var empresas = await query
                .OrderBy(e => e.Id)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToListAsync();

            return empresas.AsEnumerable();
        }

        public async Task<int> AddAsync(Empresa entity)
        {
            _context.Empresas.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Empresa entity)
        {
            _context.Empresas.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empresa = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);
            if (empresa == null)
            {
                return false;
            }
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return true;
        }

        #region Cambio_en_Base
        #endregion

    }
}
