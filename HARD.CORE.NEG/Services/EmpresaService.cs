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
    public class EmpresaService : IEmpresaService
    {
        private readonly IRepositoryBase<Empresa, BaseFilter, int> _empresaRepository;
        private readonly IRepositoryBase<Usuario, BaseFilter, int> _usuarioDA;
        private readonly ILogger<EmpresaService> _logger;

        public EmpresaService(ILogger<EmpresaService> logger,
        IRepositoryBase<Empresa, BaseFilter, int> empresaRepository,
        IRepositoryBase<Usuario, BaseFilter, int> usuarioDA)
        {
            _empresaRepository = empresaRepository;
            _usuarioDA = usuarioDA;
            _logger = logger;
        }

        #region Implementation of IServiceBase
        public async Task<WebResultModel<Empresa>> GetByIdAsync(int idEmpresa)
        {
            var webResult = new WebResultModel<Empresa>();
            try
            {
                webResult.Data = await _empresaRepository.GetByIdAsync(idEmpresa);
                webResult.Message = "Información del empresa obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información del empresa con ID: {IdEmpresa}", idEmpresa);
                webResult.Message = "Error al obtener la información del empresa.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<IEnumerable<Empresa>>> GetAllAsync(PagedFilter<BaseFilter> pagedFilter)
        {
            var webResult = new WebResultModel<IEnumerable<Empresa>>();
            try
            {
                webResult.Data = (await _empresaRepository.GetAllAsync(pagedFilter)).ToList();
                webResult.Message = "Información obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los empresaes");
                webResult.Message = "Error al obtener la información de los empresaes.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<int>> AddAsync(Empresa empresa, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                empresa.IdUsuarioCreacion = idUsuarioAuenticado;
                empresa.IdUsuarioModificacion = idUsuarioAuenticado;
                empresa.FechaCreacion = DateTime.UtcNow;
                empresa.FechaModificacion = DateTime.UtcNow;
                webResult.Data = await _empresaRepository.AddAsync(empresa);
                webResult.Message = "Empresa agregado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el empresa");
                webResult.Message = "Error al agregar el empresa.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<bool>> UpdateAsync(Empresa empresa, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                empresa.IdUsuarioModificacion = idUsuarioAuenticado;
                empresa.FechaModificacion = DateTime.UtcNow;
                await _empresaRepository.UpdateAsync(empresa);
                webResult.Data = true;
                webResult.Message = "Empresa actualizado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el empresa con ID: {IdEmpresa}", empresa.Id);
                webResult.Message = "Error al actualizar el empresa.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<bool>> DeleteAsync(int idEmpresa, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = await _empresaRepository.DeleteAsync(idEmpresa);
                webResult.Message = "Empresa eliminado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el empresa con ID: {IdEmpresa}", idEmpresa);
                webResult.Message = "Error al eliminar el empresa.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
        #endregion

        #region Implementation of IEmpresaService

        public async Task<WebResultModel<IEnumerable<Empresa>>> GetAllAsync(bool? activo = null, int? idUsuario = null, int? idPerfil = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<IEnumerable<Empresa>>();
            try
            {
                PagedFilter<BaseFilter> pagedFilter = new PagedFilter<BaseFilter>
                {
                    PageIndex = pageIndex ?? 1,
                    PageSize = pageSize ?? int.MaxValue,
                    Filters = new BaseFilter
                    {
                        IdMaster = idUsuario,
                        IdDetail = idPerfil,
                        Activo = activo
                    }
                };
                webResult = await GetAllAsync(pagedFilter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información");
                webResult.Message = "Error al obtener la información.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public async Task<WebResultModel<IEnumerable<Empresa>>> GetCompaniesByUserAsync(int idUsuario, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<IEnumerable<Empresa>>();
            try
            {
                Usuario usuario = await _usuarioDA.GetByIdAsync(idUsuario);
                List<Empresa> empresas = new List<Empresa>();
                if (usuario != null)
                {
                    usuario.Empresas ??= new List<Empresa>();
                    empresas = usuario.Empresas;
                }
                webResult.Data = empresas;
                webResult.Message = "Información de los empresas del usuario obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los empresas del usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al obtener la información de los empresas del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        #endregion

    }
}