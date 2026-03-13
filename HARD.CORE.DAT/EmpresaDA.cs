using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HARD.CORE.DAT
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

        public Empresa GetById(int id)
        {
            var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);
            return empresa;
        }

        public IEnumerable<Empresa> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            var query = _context.Empresas.AsQueryable();
            if (pagedFilter.Filters?.Activo.HasValue == true)
            {
                query = query.Where(e => e.Activo == pagedFilter.Filters.Activo.Value);
            }
            var empresas = query
                .OrderBy(e => e.Id)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToList();

            return empresas.AsEnumerable();
        }

        public int Add(Empresa entity)
        {
            try
            {
                _context.Empresas.Add(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la empresa");
                throw;
            }
        }

        public bool Update(Empresa entity)
        {
            try
            {
                _context.Empresas.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la empresa");
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);
                if (empresa == null)
                {
                    return false;
                }
                _context.Empresas.Remove(empresa);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la empresa");
                throw;
            }
        }

        #region Cambio_en_Base
        #endregion

    }
}
