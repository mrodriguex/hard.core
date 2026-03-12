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
    public class EmpresaService
    {
        private readonly IEmpresaB _empresaB;
        private readonly ILogger<EmpresaService> _logger;
        private readonly IConfiguration _config;

        public EmpresaService(ILogger<EmpresaService> logger, IEmpresaB empresaB, IConfiguration config)
        {
            _empresaB = empresaB;
            _logger = logger;
            _config = config;
        }

        public WebResultModel<Empresa> GetById(int idEmpresa)
        {
            var webResult = new WebResultModel<Empresa>();
            try
            {
                webResult.Data = _empresaB.GetById(idEmpresa);
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
        public WebResultModel<List<Empresa>> GetAll(bool? activo = null, int? idUsuario = null, int? idPerfil = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<List<Empresa>>();
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
                webResult.Data = _empresaB.GetAll(pagedFilter).ToList();
                webResult.Message = "Información obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información");
                webResult.Message = "Error al obtener la información.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Add(Empresa empresa, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                empresa.IdUsuarioCreacion = idUsuarioAuenticado;
                empresa.IdUsuarioModificacion = idUsuarioAuenticado;
                empresa.FechaCreacion = DateTime.UtcNow;
                empresa.FechaModificacion = DateTime.UtcNow;
                _empresaB.Add(empresa);
                webResult.Data = true;
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

        public WebResultModel<bool> Update(Empresa empresa, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                empresa.IdUsuarioModificacion = idUsuarioAuenticado;
                empresa.FechaModificacion = DateTime.UtcNow;
                _empresaB.Update(empresa);
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

        public WebResultModel<bool> Delete(int idEmpresa, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _empresaB.Delete(idEmpresa);
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

        public WebResultModel<List<Empresa>> GetCompaniesByUser(int idUsuario, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<List<Empresa>>();
            try
            {
                webResult.Data = _empresaB.GetCompaniesByUser(idUsuario: idUsuario);
                webResult.Message = "Información de los empresaes del usuario obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de los empresaes del usuario con ID: {IdUsuario}", idUsuario);
                webResult.Message = "Error al obtener la información de los empresaes del usuario.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

    }
}