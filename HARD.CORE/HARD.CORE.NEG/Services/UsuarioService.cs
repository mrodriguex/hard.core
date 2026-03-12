using System;
using System.Collections.Generic;
using System.Linq;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.NEG.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioB _usuarioB;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IConfiguration _config;

        public UsuarioService( ILogger<UsuarioService> logger,IUsuarioB usuarioB, IConfiguration config)
        {
            _usuarioB = usuarioB;
            _logger = logger;
            _config = config;
        }

        public WebResultModel<Usuario> GetById(int idUsuario)
        {
            var webResult = new WebResultModel<Usuario>();
            try
            {
                webResult.Data = _usuarioB.GetById(idUsuario);
                webResult.Message = "Información del usuario obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información del usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al obtener la información del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
        public WebResultModel<List<Usuario>> GetAll(bool? activo = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<List<Usuario>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    PageIndex = pageIndex ?? 1,
                    PageSize = pageSize ?? int.MaxValue,
                    Filters = new BaseFilter
                    {
                        Activo = activo
                    }
                };
                webResult.Data = _usuarioB.GetAll(pagedFilter).ToList();
                webResult.Message = "Información de los usuarios obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los usuarios");
                webResult.Message = "Error al obtener la información de los usuarios.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<int> Add(Usuario usuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                usuario.IdUsuarioCreacion = idUsuarioAutenticado;
                usuario.IdUsuarioModificacion = idUsuarioAutenticado;
                usuario.FechaCreacion = DateTime.UtcNow;
                usuario.FechaModificacion = DateTime.UtcNow;
                webResult.Data = _usuarioB.Add(usuario);
                webResult.Message = "Usuario agregado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el usuario");
                webResult.Message = "Error al agregar el usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Update(Usuario usuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                usuario.IdUsuarioModificacion = idUsuarioAutenticado;
                usuario.FechaModificacion = DateTime.UtcNow;
                _usuarioB.Update(usuario);
                webResult.Data = true;
                webResult.Message = "Usuario actualizado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario con ID: {IdUsuario}", usuario.Id);
                webResult.Message = "Error al actualizar el usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Delete(int idUsuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                _usuarioB.Delete(idUsuario);
                webResult.Data = true;
                webResult.Message = "Usuario eliminado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al eliminar el usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
        
        public WebResultModel<bool> Exists(int idUsuario)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _usuarioB.Exists(idUsuario);
                webResult.Message = "Verificación de existencia del usuario realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar la existencia del usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al verificar la existencia del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> AuthenticateUser(LoginModel login, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
            string defaultUser = _config["DefaultUser"];
                string defaultPassword = _config["DefaultPassword"];
                if (login.Username == defaultUser && login.Password == defaultPassword)
                {
                    webResult.Data = true;
                    webResult.Message = "Autenticación realizada exitosamente con usuario predeterminado.";
                    webResult.Success = true;
                    return webResult;
                }

                Usuario usuario = _usuarioB.GetByUsername(login.Username);
                bool isAuthenticated = _usuarioB.AuthenticateUser(usuario.Id, login.Password);
                if (!isAuthenticated)
                {
                    webResult.Data = false;
                    webResult.Message = "Usuario o contraseña incorrectos.";
                    webResult.Success = false;
                    return webResult;
                }

                webResult.Data = true;
                webResult.Message = "Autenticación realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la autenticación.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> UpdatePassword(LoginModel login, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                Usuario usuario = new Usuario
                {
                    IdUsuarioModificacion = idUsuarioAutenticado,
                    FechaModificacion = DateTime.UtcNow,
                    ClaveUsuario = login.Username,
                    Contrasena = login.Password
                };
                webResult.Data = _usuarioB.UpdatePassword(usuario: usuario);
                webResult.Message = "Actualización de contraseña realizada exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la actualización de contraseña.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> UnlockUser(int idUsuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                Usuario usuario = _usuarioB.GetById(idUsuario);
                usuario.IdUsuarioModificacion = idUsuarioAutenticado;
                usuario.FechaModificacion = DateTime.UtcNow;
                webResult.Data = _usuarioB.UnlockUser(usuario);
                webResult.Message = "Usuario desbloqueado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desbloquear el usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al desbloquear el usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
    }
}