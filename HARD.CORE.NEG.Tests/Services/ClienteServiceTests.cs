using System;
using System.Collections.Generic;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.NEG.Services;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HARD.CORE.NEG.Tests.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteB> _clienteBMock;
        private readonly Mock<ILogger<ClienteService>> _loggerMock;
        private readonly ClienteService _service;

        public ClienteServiceTests()
        {
            _clienteBMock = new Mock<IClienteB>();
            _loggerMock = new Mock<ILogger<ClienteService>>();
            _service = new ClienteService(_loggerMock.Object, _clienteBMock.Object);
        }

        [Fact]
        public void GetById_WhenClientExists_ReturnsSuccess()
        {
            var cliente = CreateCliente(4);

            _clienteBMock
                .Setup(x => x.GetById(4))
                .Returns(cliente);

            var result = _service.GetById(4);

            Assert.True(result.Success);
            Assert.Equal(cliente, result.Data);
            Assert.Equal("Información del cliente obtenida exitosamente.", result.Message);
            _clienteBMock.Verify(x => x.GetById(4), Times.Once);
            _clienteBMock.Verify(x => x.Add(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public void GetById_WhenBusinessThrows_ReturnsFailure()
        {
            _clienteBMock
                .Setup(x => x.GetById(4))
                .Throws(new Exception("get error"));

            var result = _service.GetById(4);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información del cliente.", result.Message);
            Assert.Contains("get error", result.Errors);
            _clienteBMock.Verify(x => x.GetById(4), Times.Once);
        }

        [Fact]
        public void GetAll_WhenClientsExist_ReturnsSuccess()
        {
            var clientes = new List<Cliente> { CreateCliente(1), CreateCliente(2) };

            _clienteBMock
                .Setup(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                    f.PageIndex == 3 &&
                    f.PageSize == 15 &&
                    f.Filters.IdMaster == 7 &&
                    f.Filters.IdDetail == 9 &&
                    f.Filters.Activo == true)))
                .Returns(clientes);

            var result = _service.GetAll(true, 7, 9, 3, 15);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información obtenida exitosamente.", result.Message);
            _clienteBMock.Verify(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                f.PageIndex == 3 &&
                f.PageSize == 15 &&
                f.Filters.IdMaster == 7 &&
                f.Filters.IdDetail == 9 &&
                f.Filters.Activo == true)), Times.Once);
            _clienteBMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetAll_WhenBusinessThrows_ReturnsFailure()
        {
            _clienteBMock
                .Setup(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()))
                .Throws(new Exception("list error"));

            var result = _service.GetAll(false, 1, 2, 1, 10);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información.", result.Message);
            Assert.Contains("list error", result.Errors);
            _clienteBMock.Verify(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Once);
        }

        [Fact]
        public void Add_WhenClientIsValid_InyectsAuditFieldsAndReturnsId()
        {
            var cliente = CreateCliente();
            Cliente? capturedCliente = null;
            var before = DateTime.UtcNow;

            _clienteBMock
                .Setup(x => x.Add(It.IsAny<Cliente>()))
                .Callback<Cliente>(value => capturedCliente = value)
                .Returns(21);

            var result = _service.Add(cliente, 50);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.Equal(21, result.Data);
            Assert.Equal("Cliente agregado exitosamente.", result.Message);
            Assert.NotNull(capturedCliente);
            Assert.Equal(50, capturedCliente.IdUsuarioCreacion);
            Assert.Equal(50, capturedCliente.IdUsuarioModificacion);
            Assert.InRange(capturedCliente.FechaCreacion, before, after);
            Assert.InRange(capturedCliente.FechaModificacion, before, after);
            _clienteBMock.Verify(x => x.Add(It.IsAny<Cliente>()), Times.Once);
            _clienteBMock.Verify(x => x.Update(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public void Add_WhenBusinessThrows_ReturnsFailure()
        {
            _clienteBMock
                .Setup(x => x.Add(It.IsAny<Cliente>()))
                .Throws(new Exception("insert error"));

            var result = _service.Add(CreateCliente(), 50);

            Assert.False(result.Success);
            Assert.Equal(0, result.Data);
            Assert.Equal("Error al agregar el cliente.", result.Message);
            Assert.Contains("insert error", result.Errors);
            _clienteBMock.Verify(x => x.Add(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public void Update_WhenClientIsValid_InyectsAuditFieldsAndReturnsSuccess()
        {
            var cliente = CreateCliente(9);
            Cliente? capturedCliente = null;
            var before = DateTime.UtcNow;

            _clienteBMock
                .Setup(x => x.Update(It.IsAny<Cliente>()))
                .Callback<Cliente>(value => capturedCliente = value)
                .Returns(true);

            var result = _service.Update(cliente, 88);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Cliente actualizado exitosamente.", result.Message);
            Assert.NotNull(capturedCliente);
            Assert.Equal(88, capturedCliente.IdUsuarioModificacion);
            Assert.InRange(capturedCliente.FechaModificacion, before, after);
            _clienteBMock.Verify(x => x.Update(It.IsAny<Cliente>()), Times.Once);
            _clienteBMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Update_WhenBusinessThrows_ReturnsFailure()
        {
            _clienteBMock
                .Setup(x => x.Update(It.IsAny<Cliente>()))
                .Throws(new Exception("update error"));

            var result = _service.Update(CreateCliente(9), 88);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al actualizar el cliente.", result.Message);
            Assert.Contains("update error", result.Errors);
            _clienteBMock.Verify(x => x.Update(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public void Delete_WhenBusinessDeletesClient_ReturnsSuccess()
        {
            _clienteBMock
                .Setup(x => x.Delete(17))
                .Returns(true);

            var result = _service.Delete(17, 88);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Cliente eliminado exitosamente.", result.Message);
            _clienteBMock.Verify(x => x.Delete(17), Times.Once);
            _clienteBMock.Verify(x => x.Add(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public void Delete_WhenBusinessThrows_ReturnsFailure()
        {
            _clienteBMock
                .Setup(x => x.Delete(17))
                .Throws(new Exception("delete error"));

            var result = _service.Delete(17, 88);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al eliminar el cliente.", result.Message);
            Assert.Contains("delete error", result.Errors);
            _clienteBMock.Verify(x => x.Delete(17), Times.Once);
        }

        private static Cliente CreateCliente(int id = 1)
        {
            return new Cliente
            {
                Id = id,
                RFC = "XAXX010101000",
                RazonSocial = "Cliente Demo"
            };
        }
    }
}
