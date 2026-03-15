
using HARD.CORE.OBJ;
using System;
using System.Data;

using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Repositories
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

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Empresas)
                .Include(u => u.Perfiles)
                .ThenInclude(p => p.Menus)
                .FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync(PagedFilter<BaseFilter> pagedFilter)
        {
            var usuarios = await _context.Usuarios
                .Where(u => (!pagedFilter.Filters.Activo.HasValue || u.Estatus ==
                            pagedFilter.Filters.Activo.Value)
                            && (string.IsNullOrEmpty(pagedFilter.Filters.Nombre) || u.ClaveUsuario.Contains(pagedFilter.Filters.Nombre)))
                .OrderBy(u => u.Id)
                .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
                .Take(pagedFilter.PageSize)
                .ToListAsync();
            return usuarios.AsEnumerable();
        }

        public async Task<int> AddAsync(Usuario entity)
        {
            foreach (var perfil in entity.Perfiles) { _context.Attach(perfil); }
            foreach (var empresa in entity.Empresas) { _context.Attach(empresa); }
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Usuario entity)
        {
            foreach (var perfil in entity.Perfiles) { _context.Attach(perfil); }
            foreach (var empresa in entity.Empresas) { _context.Attach(empresa); }
            _context.Usuarios.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario != null)
            {
                foreach (var perfil in usuario.Perfiles) { _context.Attach(perfil); }
                foreach (var empresa in usuario.Empresas) { _context.Attach(empresa); }
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #endregion
    }

}