using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.NEG
{
    public class PerfilB : IPerfilB
    {

        IPerfilDA _perfilDA;

        public PerfilB(IPerfilDA perfilDA)
        {
            _perfilDA = perfilDA;
        }

        /// <summary>
        /// Obtains a profile by its unique key.
        /// </summary>
        /// <param name="clavePerfil">The unique key identifying the profile.</param>
        /// <returns>The profile associated with the provided key.</returns>
        public Perfil Obtener(int clavePerfil)
        {
            return _perfilDA.Obtener(clavePerfil: clavePerfil);
        }


       /// <summary>
       /// Obtains all profiles.
       /// </summary>
       /// <param name="estatus">
       /// The status to filter profiles.
       /// </param>
       /// <param name="claveUsuario">
       /// The unique key identifying the user.
       /// </param>
       /// <returns>A list of profiles matching the specified criteria.</returns>
        public List<Perfil> ObtenerTodos(bool? estatus = null, string? claveUsuario = null)
        {
            if (string.IsNullOrEmpty(claveUsuario))
            {
                return _perfilDA.ObtenerTodos(estatus: estatus);
            }
            else
            {
                return _perfilDA.ObtenerPerfilesUsuario(claveUsuario: claveUsuario);
            }
        }

        /// <summary>
        /// Inserts a new profile.
        /// </summary>
        /// <param name="perfil">The profile to insert.</param>
        /// <returns>The unique key of the inserted profile.</returns>
        public int Insertar(Perfil perfil)
        {
            return _perfilDA.Insertar(perfil: perfil);
        }

        /// <summary>
        /// Updates an existing profile.
        /// </summary>
        /// <param name="perfil">The profile to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public bool Actualizar(Perfil perfil)
        {
            return _perfilDA.Actualizar(perfil: perfil);
        }

        /// <summary>
        /// Obtains the profiles associated with a specific user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>A list of profiles associated with the provided user key.</returns>
        public List<Perfil> ObtenerPerfilesUsuario(string claveUsuario)
        {
            return _perfilDA.ObtenerPerfilesUsuario(claveUsuario: claveUsuario);
        }

        /// <summary>
        /// Configures the menu for a given profile.
        /// </summary>
        /// <param name="perfil">The profile for which the menu is to be configured.</param>
        /// <returns>True if the configuration was successful; otherwise, false.</returns>
        bool IPerfilB.ConfigurarMenu_Perfil(Perfil perfil)
        {
            return _perfilDA.ConfigurarMenu_Perfil(perfil: perfil);
        }
    }
}
