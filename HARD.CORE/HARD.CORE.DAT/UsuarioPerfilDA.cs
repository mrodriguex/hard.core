// using System;
// using System.Collections.Generic;
// using System.Linq;
// using HARD.CORE.DAT.Interfaces;
// using HARD.CORE.OBJ;
// using Microsoft.Extensions.Logging;

// namespace HARD.CORE.DAT
// {
//     public class UsuarioPerfilDA : IRepositoryBase<UsuarioPerfil, BaseFilter, int>
//     {

//         private readonly HardCoreDbContext _context;
//         private readonly ILogger<UsuarioPerfilDA> _logger;
//         public UsuarioPerfilDA(HardCoreDbContext context, ILogger<UsuarioPerfilDA> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }

//         public int Add(UsuarioPerfil entity)
//         {
//             try
//             {
//                 _context.UsuarioPerfiles.Add(entity);
//                 _context.SaveChanges();
//                 return entity.IdUsuarioPerfil;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al agregar el perfil usuario");
//                 throw;
//             }
//         }

//         public bool Delete(int id)
//         {
//             try
//             {
//                 var usuarioPerfil = _context.UsuarioPerfiles.Find(id);
//                 if (usuarioPerfil == null)
//                 {
//                     return false;
//                 }
//                 _context.UsuarioPerfiles.Remove(usuarioPerfil);
//                 _context.SaveChanges();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar el perfil usuario");
//                 throw;
//             }
//         }

//         public IEnumerable<UsuarioPerfil> GetAll(PagedFilter<BaseFilter> pagedFilter)
//         {
//             var query = _context.UsuarioPerfiles.AsQueryable();

//             var usuariosPerfiles = query
//                 .Where(u => (u.IdPerfil == pagedFilter.Filters.IdDetail || !pagedFilter.Filters.IdDetail.HasValue)
//                             && (u.IdUsuario == pagedFilter.Filters.IdMaster || !pagedFilter.Filters.IdMaster.HasValue))
//                 .OrderBy(up => up.IdUsuarioPerfil)
//                 .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
//                 .Take(pagedFilter.PageSize)
//                 .ToList();

//             return usuariosPerfiles.AsEnumerable();
//         }

//         public UsuarioPerfil GetById(int id)
//         {
//             var usuarioPerfil = _context.UsuarioPerfiles.Find(id);
//             return usuarioPerfil;
//         }

//         public bool Update(UsuarioPerfil entity)
//         {
//             var usuarioPerfil = _context.UsuarioPerfiles.Find(entity.IdUsuarioPerfil);
//             if (usuarioPerfil == null)
//             {
//                 return false;
//             }

//             usuarioPerfil.IdPerfil = entity.IdPerfil;
//             usuarioPerfil.IdUsuario = entity.IdUsuario;
//             _context.UsuarioPerfiles.Update(usuarioPerfil);
//             _context.SaveChanges();
//             return true;
//         }

//     }

// }
