using System.Collections.Generic;
using System.Threading.Tasks;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IUsuarioService : IServiceBase<Usuario, Usuario, BaseFilter, int>
    {

        Task<WebResultModel<IEnumerable<Usuario>>> GetAllAsync(bool? activo = null, int? pageIndex = null, int? pageSize = null);
        /// <summary>
        /// Determines whether the user exists.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
        Task<WebResultModel<bool>> ExistsAsync(int idUsuario);

        Task<WebResultModel<Usuario>> GetByUsernameAsync(string username);

        /// <summary>
        /// Authenticates a user based on their unique key and password.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="password">The user's password.</param>
        /// <returns><c>true</c> if the user is authenticated; otherwise, <c>false</c>.</returns>
        Task<WebResultModel<bool>> AuthenticateUserAsync(LoginModel login, int idUsuarioAutenticado);


        Task<WebResultModel<bool>> UnlockUserAsync(int idUsuario, int idUsuarioAutenticado);

        /// <summary>
        /// Updates the password for a user.
        /// </summary>
        /// <param name="login">The login model containing the user's credentials.</param>
        /// <param name="idUsuarioAutenticado">The ID of the authenticated user performing the update.</param>
        /// <returns><c>true</c> if the password was updated successfully; otherwise, <c>false</c>.</returns>
        Task<WebResultModel<bool>> UpdatePasswordAsync(LoginModel login, int idUsuarioAutenticado);


    }
}