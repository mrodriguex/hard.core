using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IMenuB
    {
        /// <summary>
        /// Obtains a menu by its unique key.
        /// </summary>
        /// <param name="claveMenu">The unique key identifying the menu.</param>
        /// <returns>The menu associated with the provided key.</returns>
        Menu Obtener(int claveMenu);

        /// <summary>
        /// Obtains all menus.
        /// </summary>
        /// <param name="claveEstatus">The status key to filter menus.</param>
        /// <returns>A list of menus matching the provided status.</returns>
        List<Menu> ObtenerTodos(bool? claveEstatus = null);

        /// <summary>
        /// Obtains the menus associated with a specific profile.
        /// </summary>
        /// <param name="clavePerfil">The unique key identifying the profile.</param>
        /// <returns>A list of menus associated with the provided profile key.</returns>
        List<Menu> ObtenerMenu_Perfil(int clavePerfil);

        /// <summary>
        /// Configures the menu for a specific profile.
        /// </summary>
        /// <param name="clavePerfil">The unique key identifying the profile.</param>
        /// <param name="dtMenu">The DataTable containing the menu configuration.</param>
        /// <returns>True if the configuration was successful; otherwise, false.</returns>
        bool ConfigurarMenu_Perfil(int clavePerfil, List<Menu> menus);

        /// <summary>
        /// Inserts a new profile.
        /// </summary>
        /// <param name="Menu">The menu to insert.</param>
        /// <returns>The unique key of the inserted menu.</returns>
        int Insertar(Menu Menu);

        /// <summary>
        /// Updates an existing profile.
        /// </summary>
        /// <param name="Menu">The menu to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        bool Actualizar(Menu Menu);

        /// <summary>
        /// Obtains the menus associated with a specific user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="clavePerfil">The unique key identifying the profile.</param>
        /// <returns>A list of profiles associated with the provided user key.</returns>
        List<Menu> ObtenerMenu_Usuario(string claveUsuario, int clavePerfil);
    }
}
