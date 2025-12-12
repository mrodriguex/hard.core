using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IPerfilB
    {
        /// <summary>
        /// Obtains a profile by its unique key.
        /// </summary>
        /// <param name="clavePerfil">The unique key identifying the profile.</param>
        /// <returns>The profile associated with the provided key.</returns>
        Perfil Obtener(int clavePerfil);

        /// <summary>
        /// Obtains all profiles.
        /// </summary>
        /// <param name="claveEstatus">The status key to filter profiles.</param>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>A list of profiles matching the specified criteria.</returns>
        List<Perfil> ObtenerTodos(bool? estatus = null, string? claveUsuario = null);

        /// <summary>
        /// Inserts a new profile.
        /// </summary>
        /// <param name="perfil">The profile to insert.</param>
        /// <returns>The unique key of the inserted profile.</returns>
        int Insertar(Perfil perfil);

        /// <summary>
        /// Updates an existing profile.
        /// </summary>
        /// <param name="perfil">The profile to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        bool Actualizar(Perfil perfil);

        /// <summary>
        /// Obtains the profiles associated with a specific user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>A list of profiles associated with the provided user key.</returns>
        List<Perfil> ObtenerPerfilesUsuario(string claveUsuario);

        /// <summary>
        /// Configures the menu for a given profile.
        /// </summary>
        /// <param name="perfil">
        /// The profile for which the menu configuration is to be updated.
        /// </param>
        /// <returns>A boolean indicating whether the menu configuration was successful.</returns>
        bool ConfigurarMenu_Perfil(Perfil perfil);
    }
}
