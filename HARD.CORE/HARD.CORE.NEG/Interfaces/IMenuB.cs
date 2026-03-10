using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IMenuB: IRepositoryBase<Menu, BaseFilter, int> 
    {        
        /// <summary>
        /// Obtains the menus associated with a specific profile.
        /// </summary>
        /// <param name="idPerfil">The unique key identifying the profile.</param>
        /// <returns>A list of menus associated with the provided profile key.</returns>
        List<Menu> GetMenusByProfile(int idPerfil);

        /// <summary>
        /// Obtains the menus associated with a specific user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="idPerfil">The unique key identifying the profile.</param>
        /// <returns>A list of menus associated with the provided user and profile keys.</returns>
        List<Menu> GetMenusByUser(int idUsuario, int idPerfil);
    }
}
