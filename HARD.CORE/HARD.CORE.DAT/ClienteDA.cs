using HARD.CORE.OBJ;

using Microsoft.Extensions.Configuration;

using System;
using System.Data;
using System.Linq;

using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.DAT
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
        public IEnumerable<Cliente> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            var query = _context.Clientes.AsQueryable();
            if (pagedFilter.Filters?.Activo.HasValue == true)
            {
                query = query.Where(c => c.Activo == pagedFilter.Filters.Activo.Value);
            }
            var clientes = query
                .OrderBy(c => c.Id)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToList();

            return clientes.AsEnumerable();

        }

        public Cliente GetById(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
            return cliente;
        }

        public int Add(Cliente entity)
        {
            try
            {
                entity.FechaCreacion = DateTime.Now;
                _context.Clientes.Add(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el cliente");
                throw;
            }
        }

        public bool Update(Cliente entity)
        {
            try
            {
                entity.FechaModificacion = DateTime.Now;
                _context.Clientes.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente");
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
                if (cliente == null)
                {
                    return false;
                }
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cliente");
                throw;
            }
        }

        #endregion

    }
}