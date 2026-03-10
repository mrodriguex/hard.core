// HARD.CORE.NEG/Interfaces/IUsuarioB.cs
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IUsuarioB: IRepositoryBase<Usuario, BaseFilter, int>
    {
     
        /// <summary>
        /// Determines whether the user exists.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
        bool Exists(int idUsuario);

        /// <summary>
        /// Authenticates a user based on their unique key and password.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="password">The user's password.</param>
        /// <returns><c>true</c> if the user is authenticated; otherwise, <c>false</c>.</returns>
        bool AuthenticateUser(int idUsuario, string password);

        /// <summary>
        /// Inserts a new user.
        /// </summary>
        /// <param name="usuario">The user to insert.</param>
        /// <param name="defaultPassword">The default password for the user.</param>
        /// <returns><c>true</c> if the user was inserted successfully; otherwise, <c>false</c>.</returns>
       
        bool UnlockUser(Usuario usuario);

        /// <summary>
        /// Updates the password for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="contrasena">The new password for the user.</param>
        /// <returns><c>true</c> if the password was updated successfully; otherwise, <c>false</c>.</returns>
        bool UpdatePassword(Usuario usuario);

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user if found; otherwise, <c>null</c>.</returns>
        Usuario GetByUsername(string username);

    }
}