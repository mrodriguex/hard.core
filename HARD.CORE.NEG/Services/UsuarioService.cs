using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.NEG.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepositoryBase<Usuario, BaseFilter, int> _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IConfiguration _config;

        private readonly ICryptographerB _cryptographer;

        public UsuarioService(ILogger<UsuarioService> logger,
        IRepositoryBase<Usuario, BaseFilter, int> usuarioRepository, ICryptographerB cryptographer,
         IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _cryptographer = cryptographer;
            _config = config;
        }

        #region Implementation of IServiceBase

        public async Task<WebResultModel<Usuario>> GetByIdAsync(int idUsuario)
        {
            var webResult = new WebResultModel<Usuario>();
            try
            {
                webResult.Data = await _usuarioRepository.GetByIdAsync(idUsuario);
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
        public async Task<WebResultModel<IEnumerable<Usuario>>> GetAllAsync(PagedFilter<BaseFilter> pagedFilter)
        {
            var webResult = new WebResultModel<IEnumerable<Usuario>>();
            try
            {
                webResult.Data = (await _usuarioRepository.GetAllAsync(pagedFilter)).ToList();
                webResult.Message = "Información obtenida exitosamente.";
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

        public async Task<WebResultModel<int>> AddAsync(Usuario usuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                usuario.IdUsuarioCreacion = idUsuarioAutenticado;
                usuario.IdUsuarioModificacion = idUsuarioAutenticado;
                usuario.FechaCreacion = DateTime.UtcNow;
                usuario.FechaModificacion = DateTime.UtcNow;
                webResult.Data = await _usuarioRepository.AddAsync(usuario);
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

        public async Task<WebResultModel<bool>> UpdateAsync(Usuario usuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                usuario.IdUsuarioModificacion = idUsuarioAutenticado;
                usuario.FechaModificacion = DateTime.UtcNow;
                await _usuarioRepository.UpdateAsync(usuario);
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

        public async Task<WebResultModel<bool>> DeleteAsync(int idUsuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                await _usuarioRepository.DeleteAsync(idUsuario);
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
        #endregion

        #region Implementation of IUsuarioService

        public async Task<WebResultModel<IEnumerable<Usuario>>> GetAllAsync(bool? activo = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<IEnumerable<Usuario>>();
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
                webResult = await GetAllAsync(pagedFilter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los usuarios");
                webResult.Message = "Error al obtener la información de los usuarios.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<bool>> ExistsAsync(int idUsuario)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                Usuario usuario = await _usuarioRepository.GetByIdAsync(idUsuario);
                webResult.Data = usuario != null;
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

        public async Task<WebResultModel<Usuario>> GetByUsernameAsync(string username)
        {
            var webResult = new WebResultModel<Usuario>();
            BaseFilter baseFilter = new BaseFilter() { Nombre = username };
            PagedFilter<BaseFilter> filter = new PagedFilter<BaseFilter> { PageIndex = 1, PageSize = int.MaxValue, Filters = baseFilter };

            IEnumerable<Usuario> usuarios = await _usuarioRepository.GetAllAsync(filter);
            webResult.Data = usuarios.FirstOrDefault();
            webResult.Success = webResult.Data != null;
            webResult.Message = webResult.Success ? "Usuario encontrado." : "Usuario no encontrado.";
            return webResult;
        }

        public async Task<WebResultModel<bool>> AuthenticateUserAsync(LoginModel login, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                bool isAuthenticated = false;
                string defaultUser = _config["DefaultUser"];
                string defaultPassword = _config["DefaultPassword"];
                if (login.Username == defaultUser && login.Password == defaultPassword)
                {
                    webResult.Data = true;
                    webResult.Message = "Autenticación realizada exitosamente con usuario predeterminado.";
                    webResult.Success = true;
                    return webResult;
                }

                Usuario usuario = (await GetByUsernameAsync(login.Username)).Data;
                if (usuario == null)
                {
                    webResult.Data = false;
                    webResult.Message = "Usuario no encontrado.";
                    webResult.Success = false;
                    return webResult;
                }

                isAuthenticated = _cryptographer.CompareHash(login.Password, usuario.Contrasena);


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

                await UpdateAsync(usuario, idUsuarioAutenticado);

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

        public async Task<WebResultModel<bool>> UpdatePasswordAsync(LoginModel login, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                Usuario usuario = (await GetByUsernameAsync(login.Username)).Data;

                if (usuario == null)
                {
                    webResult.Data = false;
                    webResult.Message = "Usuario no encontrado.";
                    webResult.Success = false;
                    return webResult;
                }

                usuario.Contrasena = _cryptographer.CreateHash(input: login.Password);
                usuario.CambioContrasena = false;
                usuario.IdUsuarioModificacion = idUsuarioAutenticado;
                usuario.FechaModificacion = DateTime.UtcNow;

                webResult.Data = (await UpdateAsync(usuario, idUsuarioAutenticado)).Data;
                webResult.Message = webResult.Data ? "Actualización de contraseña realizada exitosamente." : "Error al actualizar la contraseña.";
                webResult.Success = webResult.Data;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error al realizar la actualización de contraseña.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<bool>> UnlockUserAsync(int idUsuario, int idUsuarioAutenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                Usuario usuario = await _usuarioRepository.GetByIdAsync(idUsuario);
                if (usuario == null)
                {
                    webResult.Data = false;
                    webResult.Message = "Usuario no encontrado.";
                    webResult.Success = false;
                    return webResult;
                }
                usuario.IdUsuarioModificacion = idUsuarioAutenticado;
                usuario.FechaModificacion = DateTime.UtcNow;
                webResult.Data = (await UpdateAsync(usuario, idUsuarioAutenticado)).Data;
                webResult.Message = webResult.Data ? "Usuario desbloqueado exitosamente." : "Error al desbloquear el usuario.";
                webResult.Success = webResult.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desbloquear el usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al desbloquear el usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        #endregion
    }
}