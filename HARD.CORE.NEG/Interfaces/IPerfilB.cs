using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IPerfilB : IRepositoryBase<Perfil, BaseFilter, int>
    {
        /// <summary>
        /// Obtains the profiles associated with a specific user.
        /// </summary>
        /// <param name="idUsuario">The unique key identifying the user.</param>
        /// <returns>A list of profiles associated with the specified user.</returns>
        /// <remarks>This method retrieves the user based on the provided unique key and then returns the list of profiles associated with that user. If the user does not exist, it returns an empty list.</remarks>
        /// <exception cref="ArgumentException">Thrown when the provided user key is invalid.</exception>
        List<Perfil> GetUserProfiles(int idUsuario);

        /// <summary>
        /// Assigns a profile to a user.
        /// </summary>
        /// <param name="idUsuario">The unique key identifying the user.</param>
        /// <param name="idPerfil">The unique key identifying the profile.</param>
        /// <returns>True if the profile was successfully assigned to the user; otherwise, false.</returns>
        bool AssignProfileToUser(int idUsuario, int idPerfil);

        /// <summary>
        /// Removes a profile from a user.
        /// </summary>
        /// <param name="idUsuario">The unique key identifying the user.</param>
        /// <param name="idPerfil">The unique key identifying the profile.</param>
        /// <returns>True if the profile was successfully removed from the user; otherwise, false.</returns>
        bool RemoveProfileFromUser(int idUsuario, int idPerfil);

    }
}
