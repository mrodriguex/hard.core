// HARD.CORE.NEG/Interfaces/IUsuarioB.cs
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IUsuarioB
    {
        /// <summary>   
        /// Obtains a user by their unique key.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>The user associated with the provided key.</returns>
        Usuario Obtener(string claveUsuario);

        /// <summary>
        /// Obtains all users.
        /// </summary>
        /// <returns>A DataTable containing all users.</returns>
        List<Usuario> ObtenerTodos();

        /// <summary>
        /// Obtains all users from the Active Directory.    
        /// </summary>
        /// <returns>A DataTable containing all users from the Active Directory.</returns>
        List<Usuario> ObtenerUsuariosDirectorioActivo();

        /// <summary>
        /// Obtains a user from the Active Directory by their unique key.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>The user associated with the provided key.</returns>
        Usuario ObtenerUsuarioDirectorioActivo(string claveUsuario);

        List<Usuario> ObtenerUsuariosDirectorioActivoTodos();

        /// <summary>
        /// Determines whether the user can change their password.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns><c>true</c> if the user can change their password; otherwise, <c>false</c>.</returns>
        bool PuedeCambiarContrasena(string claveUsuario);

        /// <summary>
        /// Determines whether the user exists.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
        bool ExisteUsuario(string claveUsuario);

        /// <summary>
        /// Obtains a suggested username based on the provided names.
        /// </summary>
        /// <param name="apellidoPaterno">The user's last name.</param>
        /// <param name="apellidoMaterno">The user's mother's last name.</param>
        /// <param name="nombres">The user's first names.</param>
        /// <returns>A suggested username.</returns>
        string ObtenerUsuarioSugerido(string apellidoPaterno, string apellidoMaterno, string nombres);

        /// <summary>
        /// Authenticates a user based on their unique key and password.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="password">The user's password.</param>
        /// <returns><c>true</c> if the user is authenticated; otherwise, <c>false</c>.</returns>
        bool AutenticarUsuario(string claveUsuario, string password);

        /// <summary>
        /// Registers user activity.
        /// </summary>
        /// <param name="bitacoraEventos">
        /// The event log entry to register.
        /// </param>
        /// <returns><c>true</c> if the activity was registered successfully; otherwise, <c>false</c>.</returns>
        public bool RegistrarActividad(BitacoraEventos bitacoraEventos, int tipoRegistro);

        /// <summary>
        /// Updates the login attempt count for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="numeroIntentos">The number of login attempts.</param>
        /// <returns><c>true</c> if the login attempt count was updated successfully; otherwise, <c>false</c>.</returns>
        bool ActualizaIntento(string claveUsuario);

        /// <summary>
        /// Resets the login attempt count for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="numeroIntentos">The number of login attempts.</param>   
        /// <returns><c>true</c> if the login attempt count was reset successfully; otherwise, <c>false</c>.</returns>
        bool ReiniciaIntento(string claveUsuario);

        /// <summary>
        /// Inserts a new user.
        /// </summary>
        /// <param name="usuario">The user to insert.</param>
        /// <param name="defaultPassword">The default password for the user.</param>
        /// <returns><c>true</c> if the user was inserted successfully; otherwise, <c>false</c>.</returns>
        bool Insertar(Usuario usuario, string defaultPassword);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="usuario">
        /// The user to update.
        /// </param>
        /// <returns><c>true</c> if the user was updated successfully; otherwise, <c>false</c>.</returns>
        bool Actualizar(Usuario usuario);

        /// <summary>
        /// Unlocks a user.
        /// </summary>
        /// <param name="usuario">
        /// The user to unlock.
        /// </param>
        /// <returns><c>true</c> if the user was unlocked successfully; otherwise, <c>false</c>.</returns>
        bool Desbloquear(Usuario usuario);

        /// <summary>
        /// Updates the password for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="contrasena">The new password for the user.</param>
        /// <returns><c>true</c> if the password was updated successfully; otherwise, <c>false</c>.</returns>
        bool ActualizaContrasena(Usuario usuario);

        /// <summary>
        /// Obtiene la actividad de los usuarios.
        /// </summary>
        /// <param name="claveUsuario">
        /// The unique key identifying the user.    
        /// </param>
        /// <param name="fechaInicial">
        /// The start date for the activity log.
        /// </param>
        /// <param name="fechaFinal">
        /// The end date for the activity log.
        /// </param>
        /// <returns>
        /// A list of event log entries matching the specified criteria.
        /// </returns>
        List<BitacoraAcessos> ObtenerActividad(string? claveUsuario = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null);

    }
}