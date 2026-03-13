using System;
using System.Collections.Generic;
using System.Linq;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.NEG.Services
{
    public class ClienteService
    {
        private readonly IClienteB _clienteB;
        private readonly ILogger<ClienteService> _logger;        

        public ClienteService(ILogger<ClienteService> logger, IClienteB clienteB)
        {
            _clienteB = clienteB;
            _logger = logger;
        }

        public WebResultModel<Cliente> GetById(int idCliente)
        {
            var webResult = new WebResultModel<Cliente>();
            try
            {
                webResult.Data = _clienteB.GetById(idCliente);
                webResult.Message = "Información del cliente obtenida exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información del cliente con ID: {IdCliente}", idCliente);
                webResult.Message = "Error al obtener la información del cliente.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }
        public WebResultModel<List<Cliente>> GetAll(bool? activo = null, int? idUsuario = null, int? idPerfil = null, int? pageIndex = null, int? pageSize = null)
        {
            var webResult = new WebResultModel<List<Cliente>>();
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
                webResult.Data = _clienteB.GetAll(pagedFilter).ToList();
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

        public WebResultModel<int> Add(Cliente cliente, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<int>();
            try
            {
                cliente.IdUsuarioCreacion = idUsuarioAuenticado;
                cliente.IdUsuarioModificacion = idUsuarioAuenticado;
                cliente.FechaCreacion = DateTime.UtcNow;
                cliente.FechaModificacion = DateTime.UtcNow;
                webResult.Data = _clienteB.Add(cliente);
                webResult.Message = "Cliente agregado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el cliente");
                webResult.Message = "Error al agregar el cliente.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Update(Cliente cliente, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                cliente.IdUsuarioModificacion = idUsuarioAuenticado;
                cliente.FechaModificacion = DateTime.UtcNow;
                _clienteB.Update(cliente);
                webResult.Data = true;
                webResult.Message = "Cliente actualizado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente con ID: {IdCliente}", cliente.Id);
                webResult.Message = "Error al actualizar el cliente.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

        public WebResultModel<bool> Delete(int idCliente, int idUsuarioAuenticado)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                webResult.Data = _clienteB.Delete(idCliente);
                webResult.Message = "Cliente eliminado exitosamente.";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cliente con ID: {IdCliente}", idCliente);
                webResult.Message = "Error al eliminar el cliente.";
                webResult.Errors.Add(ex.Message);
            }
            return webResult;
        }

    }
}