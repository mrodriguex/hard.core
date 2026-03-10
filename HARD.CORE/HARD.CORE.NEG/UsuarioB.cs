using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace HARD.CORE.NEG
{
    /// <summary>
    /// Business logic layer for managing users.
    /// </summary>
    public class UsuarioB : IUsuarioB
    {
        private readonly IRepositoryBase<Usuario, BaseFilter, int> _usuarioDA;
        private readonly ICryptographer _cryptographer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsuarioB"/> class.
        /// </summary>
        /// <param name="usuarioDA">Data access layer for user operations.</param>
        /// <param name="correoB">Business logic for email operations.</param>
        /// <param name="cryptographer">Service for cryptographic operations.</param>
        /// <param name="perfilB">Business logic for user profiles.</param>
        public UsuarioB(IRepositoryBase<Usuario, BaseFilter, int> usuarioDA,
                       ICryptographer cryptographer)
        {
            _usuarioDA = usuarioDA;
            _cryptographer = cryptographer;
        }

        /// <summary>
        /// Retrieves a <see cref="Usuario"/> object based on the specified user key.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user to retrieve.</param>
        /// <returns>
        /// The <see cref="Usuario"/> object corresponding to the provided key, or <c>null</c> if no user is found.
        /// </returns>
        public Usuario GetById(int idUsuario)
        {
            return _usuarioDA.GetById(idUsuario);
        }

        /// <summary>
        /// Retrieves all user records from the data source.
        /// </summary>
        /// <returns>A list of <see cref="Usuario"/> objects representing all users.</returns>

        public IEnumerable<Usuario> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            return _usuarioDA.GetAll(pagedFilter);
        }

        /// <summary>
        /// Updates an existing user in the system.
        /// </summary>
        /// <param name="usuario">The user to update.</param>
        public bool Update(Usuario usuario)
        {
            Usuario usuarioModificacion = _usuarioDA.GetById(usuario.IdUsuario);
            usuarioModificacion.Nombre = usuario.Nombre;
            usuarioModificacion.ApellidoPaterno = usuario.ApellidoPaterno;
            usuarioModificacion.ApellidoMaterno = usuario.ApellidoMaterno;
            usuarioModificacion.Correo = usuario.Correo;
            usuarioModificacion.Bloqueado = usuario.Bloqueado;
            usuarioModificacion.CambioContrasena = usuario.CambioContrasena;
            usuarioModificacion.Activo = usuario.Activo;
            usuarioModificacion.Estatus = usuario.Estatus;
            usuarioModificacion.Perfiles = usuario.Perfiles;
            usuarioModificacion.Empresas = usuario.Empresas;
            usuarioModificacion.IdUsuarioModificacion = usuario.IdUsuarioModificacion;

            return _usuarioDA.Update(usuarioModificacion);
        }

        public int Add(Usuario entity)
        {
            return _usuarioDA.Add(entity);
        }

        public bool Delete(int id)
        {
            Usuario usuario = _usuarioDA.GetById(id);
            if (usuario != null)
            {
                return _usuarioDA.Delete(id);
            }
            return false;
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary> <param name="username">The username of the user to retrieve.</param>
        /// <returns>
        /// The <see cref="Usuario"/> object corresponding to the provided username, or <c>null</c> if no user is found.
        /// </returns>
        public Usuario GetByUsername(string username)
        {
            BaseFilter baseFilter = new BaseFilter() { Nombre = username };
            PagedFilter<BaseFilter> filter = new PagedFilter<BaseFilter> { PageIndex = 1, PageSize = int.MaxValue, Filters = baseFilter };

            List<Usuario> usuarios = _usuarioDA.GetAll(filter).ToList();
            return usuarios.FirstOrDefault();
        }

        /// <summary>
        /// Checks if a user exists in the system.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>
        /// <c>true</c> if the user exists; otherwise, <c>false</c>.
        /// </returns>
        public bool Exists(int idUsuario)
        {
            Usuario usuario = _usuarioDA.GetById(idUsuario);
            return usuario != null;
        }

        /// <summary>
        /// Authenticates a user against the local database.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns><c>true</c> if the user is authenticated; otherwise, <c>false</c>.</returns>
        public bool AuthenticateUser(int idUsuario, string password)
        {
            bool isAuthenticated = false;
            Usuario usuario = _usuarioDA.GetById(idUsuario);


            isAuthenticated = _cryptographer.CompareHash("SHA512CryptoServiceProvider", password, usuario.Contrasena);


            if (isAuthenticated)
            {
                usuario.NumeroIntentos = 0;
            }

            else
            {
                usuario.NumeroIntentos++;
            }

            if (usuario.NumeroIntentos >= 3)
            {
                usuario.Bloqueado = true;
            }

            _usuarioDA.Update(usuario);

            return isAuthenticated;
        }

        /// <summary>
        /// Unlocks a user.
        /// </summary>
        /// <param name="usuario"></param>
        public bool UnlockUser(Usuario usuario)
        {
            bool result = false;
            if (usuario != null)
            {
                usuario.Bloqueado = false;
                usuario.NumeroIntentos = 0;
                result = _usuarioDA.Update(usuario);
            }
            return result;
        }

        /// <summary>
        /// Updates the password for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="contrasena">The new password for the user.</param>
        public bool UpdatePassword(Usuario usuario)
        {
            string hash = _cryptographer.CreateHash(algorithmName: "SHA512CryptoServiceProvider", plainText: usuario.Contrasena);
            Usuario usuarioModificacion = _usuarioDA.GetById(usuario.IdUsuario);
            usuarioModificacion.Contrasena = hash;
            usuarioModificacion.CambioContrasena = false;
            return _usuarioDA.Update(usuarioModificacion);
        }

    }
}
