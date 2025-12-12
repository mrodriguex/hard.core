using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System.Data;
using HARD.CORE.DAT.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using HARD.CORE.OBJ.Enums;
using System.Linq;

namespace HARD.CORE.NEG
{
    /// <summary>
    /// Business logic layer for managing users.
    /// </summary>
    public class UsuarioB : IUsuarioB
    {
        private readonly IUsuarioDA _usuarioDA;
        private readonly ICorreoB _correoB;
        private readonly IPerfilB _perfilB;
        private readonly IBitacoraEventosB _bitacoraEventosB;
        private readonly ICryptographer _cryptographer;
        private readonly ILdapAuthentication _ldapAuthentication;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsuarioB"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration settings.</param>
        /// <param name="usuarioDA">Data access layer for user operations.</param>
        /// <param name="correoB">Business logic for email operations.</param>
        /// <param name="cryptographer">Service for cryptographic operations.</param>
        /// <param name="ldapAuthentication">Service for LDAP authentication.</param>
        /// <param name="perfilB">Business logic for user profiles.</param>
        public UsuarioB(IConfiguration configuration, IUsuarioDA usuarioDA, ICorreoB correoB,
                       ICryptographer cryptographer, ILdapAuthentication ldapAuthentication, IPerfilB perfilB, IBitacoraEventosB bitacoraEventosB)
        {
            _usuarioDA = usuarioDA;
            _correoB = correoB;
            _cryptographer = cryptographer;
            _ldapAuthentication = ldapAuthentication;
            _perfilB = perfilB;
            _bitacoraEventosB = bitacoraEventosB;
        }

        /// <summary>
        /// Retrieves a <see cref="Usuario"/> object based on the specified user key.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user to retrieve.</param>
        /// <returns>
        /// The <see cref="Usuario"/> object corresponding to the provided key, or <c>null</c> if no user is found.
        /// </returns>
        public Usuario Obtener(string claveUsuario)
        {
            return _usuarioDA.Obtener(claveUsuario);
        }

        /// <summary>
        /// Retrieves all user records from the data source.
        /// </summary>
        /// <returns>A list of <see cref="Usuario"/> objects representing all users.</returns>
        public List<Usuario> ObtenerTodos()
        {
            return _usuarioDA.ObtenerTodos();
        }

        /// <summary>
        /// Retrieves a list of users from the Active Directory.
        /// </summary>
        /// <returns>A list of <see cref="Usuario"/> objects representing users from the Active Directory.</returns>
        public List<Usuario> ObtenerUsuariosDirectorioActivo()
        {
            return _usuarioDA.ObtenerUsuariosDirectorioActivo();
        }

        /// <summary>
        /// Retrieves a <see cref="Usuario"/> object from the Active Directory based on the specified user key.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user to retrieve.</param>
        /// <returns>
        /// The <see cref="Usuario"/> object corresponding to the provided key, or <c>null</c> if no user is found.
        /// </returns>
        public Usuario ObtenerUsuarioDirectorioActivo(string claveUsuario)
        {
            return _usuarioDA.ObtenerUsuariosDirectorioActivo(claveUsuario);
        }

        /// <summary>
        /// Retrieves a <see cref="Usuario"/> object from the Active Directory based on the specified user key.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user to retrieve.</param>
        /// <returns>
        /// The <see cref="Usuario"/> object corresponding to the provided key, or <c>null</c> if no user is found.
        /// </returns>
        public List<Usuario> ObtenerUsuariosDirectorioActivoTodos()
        {
            return _usuarioDA.ObtenerUsuariosDirectorioActivoTodos();
        }

        /// <summary>
        /// Determines whether the user can change their password.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>
        /// <c>true</c> if the user can change their password; otherwise, <c>false</c>.
        /// </returns>
        public bool PuedeCambiarContrasena(string claveUsuario)
        {
            Usuario usuario = _usuarioDA.Obtener(claveUsuario);
            return !usuario.esActive;
        }

        /// <summary>
        /// Checks if a user exists in the system.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <returns>
        /// <c>true</c> if the user exists; otherwise, <c>false</c>.
        /// </returns>
        public bool ExisteUsuario(string claveUsuario)
        {
            return _usuarioDA.ExisteUsuario(claveUsuario);
        }

        /// <summary>
        /// Suggests a unique username based on the provided names.
        /// </summary>
        /// <param name="apellidoPaterno">The paternal surname of the user.</param>
        /// <param name="apellidoMaterno">The maternal surname of the user.</param>
        /// <param name="nombres">The first names of the user.</param>
        /// <returns>A suggested unique username.</returns>
        public string ObtenerUsuarioSugerido(string apellidoPaterno, string apellidoMaterno, string nombres)
        {
            int i = 0;
            int j = 0;
            string[] arrayNombres = nombres.Split(' ');
            string nombre = null;

            apellidoPaterno = FormatoCadena(apellidoPaterno);
            apellidoMaterno = FormatoCadena(apellidoMaterno);
            nombres = FormatoCadena(nombres);

            for (j = -1; j <= apellidoMaterno.Length - 1; j += 1)
            {
                for (i = 0; i <= arrayNombres[0].Length - 1; i += 1)
                {
                    if (j >= 0)
                    {
                        nombre = arrayNombres[0].Substring(0, i + 1) + apellidoMaterno.Substring(0, j + 1) + apellidoPaterno;
                        nombre = nombre.ToLower();
                        if (!_usuarioDA.ExisteUsuario(nombre))
                        {
                            return FormatoCadena(nombre);
                        }
                    }
                    else
                    {
                        nombre = arrayNombres[0].Substring(0, i + 1) + apellidoPaterno;
                        nombre = nombre.ToLower();
                        if (!_usuarioDA.ExisteUsuario(nombre))
                        {
                            return FormatoCadena(nombre);
                        }
                    }
                }
            }

            return "";

        }

        /// <summary>
        /// Suggests a unique username based on the provided names.
        /// </summary>
        /// <param name="texto">The text to format.</param>
        /// <returns>A formatted string.</returns>
        private string FormatoCadena(string texto)
        {
            texto = texto.Replace("á", "a");
            texto = texto.Replace("é", "e");
            texto = texto.Replace("í", "i");
            texto = texto.Replace("ó", "o");
            texto = texto.Replace("ú", "u");
            texto = texto.Replace(" ", "");
            texto = texto.Replace("-", "");
            texto = texto.Replace("_", "");

            return texto;
        }

        /// <summary>
        /// Authenticates a user against the LDAP directory.   
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns><c>true</c> if the user is authenticated; otherwise, <c>false</c>.</returns>
        public bool AutenticarUsuarioLDAP(string claveUsuario, string password)
        {
            return _ldapAuthentication.IsAutheticated(domain: "CRYO_DOMAIN", username: claveUsuario, pwd: password);
        }

        /// <summary>
        /// Authenticates a user against the local database.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns><c>true</c> if the user is authenticated; otherwise, <c>false</c>.</returns>
        public bool AutenticarUsuario(string claveUsuario, string password)
        {
            bool isAuthenticated = false;
            Usuario usuario = _usuarioDA.Obtener(claveUsuario);

            if (usuario.esActive)
            {
                isAuthenticated = AutenticarUsuarioLDAP(claveUsuario: claveUsuario, password: password);
            }
            else
            {
                isAuthenticated = _cryptographer.CompareHash("SHA512CryptoServiceProvider", password, _usuarioDA.AutenticarUsuario(claveUsuario));
            }

            if (isAuthenticated)
            {
                _usuarioDA.ReiniciaIntento(claveUsuario: usuario.ClaveUsuario);
            }

            else
            {
                _usuarioDA.ActualizaIntento(claveUsuario: usuario.ClaveUsuario);
            }

            return isAuthenticated;
        }

        /// <summary>
        /// Registers user activity.
        /// </summary>
        /// <param name="bitacoraEventos">
        /// The event log entry to register.
        /// </param>
        public bool RegistrarActividad(BitacoraEventos bitacoraEventos, int tipoRegistro)
        {
            bitacoraEventos.ClaveEntidad = (int)Entidad.Usuario;
            bitacoraEventos.ClaveEvento = tipoRegistro == 1 ? (int)Evento.AutenticacionInicioSesionExitoso : (int)Evento.AutenticacionTokenExpirado;
            bitacoraEventos.Descripcion = tipoRegistro == 1 ? "Inicio de sesión exitoso." : "Token expirado.";
            bitacoraEventos.ClaveTipoEvento = (int)TipoEvento.Informacion;
            return _bitacoraEventosB.Insertar(bitacoraEventos) > 0;
        }

        /// <summary>
        /// Updates the login attempt count for a user. 
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="numeroIntentos">The number of login attempts.</param>
        public bool ActualizaIntento(string claveUsuario)
        {
            _usuarioDA.ActualizaIntento(claveUsuario: claveUsuario);
            return true;
        }

        /// <summary>
        /// Resets the login attempt count for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        public bool ReiniciaIntento(string claveUsuario)
        {
            _usuarioDA.ReiniciaIntento(claveUsuario: claveUsuario);
            return true;
        }

        /// <summary>
        /// Inserts a new user into the system.
        /// </summary>
        /// <param name="usuario">The user to insert.</param>
        /// <param name="defaultPassword">The default password for the user.</param>
        public bool Insertar(Usuario usuario, string defaultPassword)
        {
            string hash = _cryptographer.CreateHash(algorithmName: "SHA512CryptoServiceProvider", plainText: defaultPassword);
            usuario.Contrasena = hash;
            usuario.CambioContrasena = !usuario.esActive;
            return _usuarioDA.Insertar(usuario);
        }

        /// <summary>
        /// Updates an existing user in the system.
        /// </summary>
        /// <param name="usuario">The user to update.</param>
        public bool Actualizar(Usuario usuario)
        {
            Usuario usuarioModificacion = _usuarioDA.Obtener(usuario.ClaveUsuario);
            usuarioModificacion.Nombre = usuario.Nombre;
            usuarioModificacion.ApellidoPaterno = usuario.ApellidoPaterno;
            usuarioModificacion.ApellidoMaterno = usuario.ApellidoMaterno;
            usuarioModificacion.Correo = usuario.Correo;
            usuarioModificacion.Bloqueado = usuario.Bloqueado;
            usuarioModificacion.CambioContrasena = usuario.CambioContrasena;
            usuarioModificacion.esActive = usuario.esActive;
            usuarioModificacion.Estatus = usuario.Estatus;
            usuarioModificacion.Perfiles = usuario.Perfiles;
            usuarioModificacion.Empresas = usuario.Empresas;
            usuarioModificacion.EmpresaActivo = usuario.EmpresaActivo;
            usuarioModificacion.ClaveUsuarioUltimaActualizacion = usuario.ClaveUsuarioUltimaActualizacion;

            return _usuarioDA.Actualizar(usuarioModificacion);
        }

        /// <summary>
        /// Unlocks a user.
        /// </summary>
        /// <param name="usuario"></param>
        public bool Desbloquear(Usuario usuario)
        {
            bool result = false;
            if (ExisteUsuario(usuario.ClaveUsuario))
            {
                result = _usuarioDA.Desbloquear(claveUsuario: usuario.ClaveUsuario);
            }
            return result;
        }

        /// <summary>
        /// Updates the password for a user.
        /// </summary>
        /// <param name="claveUsuario">The unique key identifying the user.</param>
        /// <param name="contrasena">The new password for the user.</param>
        public bool ActualizaContrasena(Usuario usuario)
        {
            Usuario usuarioModificacion = _usuarioDA.Obtener(usuario.ClaveUsuario);               
            usuarioModificacion.ClaveUsuarioUltimaActualizacion = usuario.ClaveUsuarioUltimaActualizacion;
            usuarioModificacion.Contrasena = _cryptographer.CreateHash(algorithmName: "SHA512CryptoServiceProvider", plainText: usuario.Contrasena); // Aquí deberías generar o recibir la nueva contraseña
            usuarioModificacion.CambioContrasena = false;
            usuarioModificacion.Bloqueado = false;
            usuarioModificacion.NumeroIntentos = 0;
            return _usuarioDA.Actualizar(usuarioModificacion);
        }
      
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
        public List<BitacoraAcessos> ObtenerActividad(string? claveUsuario = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            List<Usuario> usuarios = _usuarioDA.ObtenerTodos();
            List<BitacoraEventos> bitacoraEventos = _bitacoraEventosB.ObtenerTodos(claveEvento: (int)Evento.AutenticacionInicioSesionExitoso,
                                                                                      claveEntidad: (int)Entidad.Usuario,
                                                                                     fechaInicial: fechaInicial,
                                                                                     fechaFinal: fechaFinal);

            if (!string.IsNullOrEmpty(claveUsuario))
            {
                bitacoraEventos = bitacoraEventos.FindAll(be => be.ClaveUsuario == claveUsuario);
            }

            List<BitacoraAcessos> bitacoraAcessos = bitacoraEventos
                .GroupBy(b => b.ClaveUsuario)
                    .Select(g => new BitacoraAcessos
                    {
                        ClaveUsuario = g.Key,
                        NombreUsuario = usuarios.FindLast(u => u.ClaveUsuario == g.Key)?.NombreCompleto,
                        NumeroIngresos = g.Count(),
                        UltimaConexion = g.Max(b => b.Fecha) ?? DateTime.MinValue
                    }).ToList();

            return bitacoraAcessos;
        }

    }
}
