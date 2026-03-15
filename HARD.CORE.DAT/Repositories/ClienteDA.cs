using HARD.CORE.OBJ;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using System.Linq;

using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HARD.CORE.DAT.Repositories
{
    public class ClienteDA : IRepositoryBase<Cliente, BaseFilter, int>
    {

        private readonly HardCoreDbContext _context;
        private readonly ILogger<ClienteDA> _logger;

        public ClienteDA(HardCoreDbContext context, ILogger<ClienteDA> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Public  

        public async Task<Cliente> GetByIdAsync(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync(PagedFilter<BaseFilter> pagedFilter)
        {
            var query = _context.Clientes.AsQueryable();
            if (pagedFilter.Filters?.Activo.HasValue == true)
            {
                query = query.Where(c => c.Activo == pagedFilter.Filters.Activo.Value);
            }
            var clientes = await query
                .OrderBy(c => c.Id)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToListAsync();

            return clientes.AsEnumerable();

        }

        public async Task<int> AddAsync(Cliente entity)
        {
            _context.Clientes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Cliente entity)
        {
            _context.Clientes.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null)
            {
                return false;
            }
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

    }
}