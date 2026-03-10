// using System;
// using System.Collections.Generic;
// using System.Linq;
// using HARD.CORE.DAT.Interfaces;
// using HARD.CORE.OBJ;
// using Microsoft.Extensions.Logging;

// namespace HARD.CORE.DAT
// {
//     public class PerfilMenuDA : IRepositoryBase<PerfilMenu, BaseFilter, int>
//     {

//         private readonly HardCoreDbContext _context;
//         private readonly ILogger<PerfilMenuDA> _logger;
//         public PerfilMenuDA(HardCoreDbContext context, ILogger<PerfilMenuDA> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }

//         public int Add(PerfilMenu entity)
//         {
//             try
//             {
//                 _context.PerfilMenus.Add(entity);
//                 _context.SaveChanges();
//                 return entity.IdPerfilMenu;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al agregar el perfil menú");
//                 throw;
//             }
//         }

//         public bool Delete(int id)
//         {
//             try
//             {
//                 var perfilMenu = _context.PerfilMenus.Find(id);
//                 if (perfilMenu == null)
//                 {
//                     return false;
//                 }
//                 _context.PerfilMenus.Remove(perfilMenu);
//                 _context.SaveChanges();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar el perfil menú");
//                 throw;
//             }
//         }

//         public IEnumerable<PerfilMenu> GetAll(PagedFilter<BaseFilter> pagedFilter)
//         {
//             var query = _context.PerfilMenus.AsQueryable();

//             var menus = query
//             .Where(pm => (!pagedFilter.Filters.IdMaster.HasValue || pm.IdPerfil ==
//                         pagedFilter.Filters.IdMaster.Value)
//                         && (!pagedFilter.Filters.IdDetail.HasValue || pm.IdMenu == pagedFilter.Filters.IdDetail.Value))
//                 .OrderBy(m => m.IdPerfilMenu)
//                 .Skip((pagedFilter.PageIndex - 1) * pagedFilter.PageSize)
//                 .Take(pagedFilter.PageSize)
//                 .ToList();

//             return menus.AsEnumerable();
//         }

//         public PerfilMenu GetById(int id)
//         {
//             var perfilMenu = _context.PerfilMenus.Find(id);
//             return perfilMenu;
//         }

//         public bool Update(PerfilMenu entity)
//         {
//             var perfilMenu = _context.PerfilMenus.Find(entity.IdPerfilMenu);
//             if (perfilMenu == null)
//             {
//                 return false;
//             }

//             perfilMenu.IdPerfil = entity.IdPerfil;
//             perfilMenu.IdMenu = entity.IdMenu;
//             _context.PerfilMenus.Update(perfilMenu);
//             _context.SaveChanges();
//             return true;
//         }

//     }

// }
