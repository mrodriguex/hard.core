using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HARD.CORE.DAT
{
    public class PerfilDA : IRepositoryBase<Perfil, BaseFilter, int>
    {

        private readonly HardCoreDbContext _context;
        private readonly ILogger<PerfilDA> _logger;

        public PerfilDA(HardCoreDbContext context, ILogger<PerfilDA> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Private


        public Perfil GetById(int id)
        {
            var perfil = _context.Perfiles
                .Include(p => p.Menus)
                .FirstOrDefault(p => p.Id == id);
            return perfil;
        }

        public IEnumerable<Perfil> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            var perfiles = _context.Perfiles
                .Where(p => (!pagedFilter.Filters.Activo.HasValue || p.Activo == pagedFilter.Filters.Activo.Value)
                            && (string.IsNullOrEmpty(pagedFilter.Filters.Nombre) || p.Nombre.Contains(pagedFilter.Filters.Nombre)))
                .OrderBy(p => p.Id)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToList();
            return perfiles.AsEnumerable();
        }

        public int Add(Perfil entity)
        {
            try
            {
                entity.FechaCreacion = DateTime.Now;
                foreach (var menu in entity.Menus) { _context.Attach(menu); }
                _context.Perfiles.Add(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el perfil");
                throw;
            }
        }

        public bool Update(Perfil entity)
        {
            try
            {
                entity.FechaModificacion = DateTime.Now;
                foreach (var menu in entity.Menus) { _context.Attach(menu); }
                _context.Perfiles.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el perfil");
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var perfil = _context.Perfiles.FirstOrDefault(p => p.Id == id);
                if (perfil != null)
                {
                    foreach (var menu in perfil.Menus) { _context.Attach(menu); }
                    _context.Perfiles.Remove(perfil);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el perfil");
                throw;
            }
        }
        #endregion

    }
}
