
using HARD.CORE.OBJ;
using System;
using System.Data;

using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.DAT
{
    public class UsuarioDA : IRepositoryBase<Usuario, BaseFilter, int>
    {

        private readonly HardCoreDbContext _context;
        private readonly ILogger<UsuarioDA> _logger;

        public UsuarioDA(HardCoreDbContext context, ILogger<UsuarioDA> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Public

        public Usuario GetById(int id)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Empresas)
                .Include(u => u.Perfiles)
                .ThenInclude(p => p.Menus)
                .FirstOrDefault(u => u.IdUsuario == id);
            return usuario;
        }

        public IEnumerable<Usuario> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            var usuarios = _context.Usuarios
                .Where(u => (!pagedFilter.Filters.Activo.HasValue || u.Estatus ==
                            pagedFilter.Filters.Activo.Value)
                            && (string.IsNullOrEmpty(pagedFilter.Filters.Nombre) || u.ClaveUsuario.Contains(pagedFilter.Filters.Nombre)))
                .OrderBy(u => u.IdUsuario)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToList();
            return usuarios.AsEnumerable();
        }

        public int Add(Usuario entity)
        {
            try
            {
                entity.FechaCreacion = DateTime.Now;
                foreach (var perfil in entity.Perfiles) { _context.Attach(perfil); }
                foreach (var empresa in entity.Empresas) { _context.Attach(empresa); }
                _context.Usuarios.Add(entity);
                _context.SaveChanges();
                return entity.IdUsuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el usuario");
                throw;
            }
        }

        public bool Update(Usuario entity)
        {
            try
            {
                entity.FechaModificacion = DateTime.Now;
                foreach (var perfil in entity.Perfiles) { _context.Attach(perfil); }
                foreach (var empresa in entity.Empresas) { _context.Attach(empresa); }
                _context.Usuarios.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario");
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
                if (usuario != null)
                {
                    foreach (var perfil in usuario.Perfiles) { _context.Attach(perfil); }
                    foreach (var empresa in usuario.Empresas) { _context.Attach(empresa); }
                    _context.Usuarios.Remove(usuario);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario");
                throw;
            }
        }

        #endregion
    }

}