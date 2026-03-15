using System;
using System.Collections.Generic;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.NEG.Services;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HARD.CORE.NEG.Tests.Services
{
    public class EmpresaServiceTests
    {
        private readonly Mock<IEmpresaB> _empresaBMock;
        private readonly Mock<ILogger<EmpresaService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly EmpresaService _service;

        public EmpresaServiceTests()
        {
            _empresaBMock = new Mock<IEmpresaB>();
            _loggerMock = new Mock<ILogger<EmpresaService>>();
            _configurationMock = new Mock<IConfiguration>();
            _service = new EmpresaService(_loggerMock.Object, _empresaBMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void GetById_WhenCompanyExists_ReturnsSuccess()
        {
            var empresa = CreateEmpresa(3);

            _empresaBMock
                .Setup(x => x.GetByIdAsync(3))
                .Returns(empresa);

            var result = _service.GetByIdAsync(3);

            Assert.True(result.Success);
            Assert.Equal(empresa, result.Data);
            Assert.Equal("Información del empresa obtenida exitosamente.", result.Message);
            _empresaBMock.Verify(x => x.GetByIdAsync(3), Times.Once);
            _empresaBMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetById_WhenBusinessThrows_ReturnsFailure()
        {
            _empresaBMock
                .Setup(x => x.GetByIdAsync(3))
                .Throws(new Exception("get error"));

            var result = _service.GetByIdAsync(3);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información del empresa.", result.Message);
            Assert.Contains("get error", result.Errors);
            _empresaBMock.Verify(x => x.GetByIdAsync(3), Times.Once);
        }

        [Fact]
        public void GetAll_WhenCompaniesExist_ReturnsSuccess()
        {
            var empresas = new List<Empresa> { CreateEmpresa(1), CreateEmpresa(2) };

            _empresaBMock
                .Setup(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                    f.PageIndex == 2 &&
                    f.PageSize == 25 &&
                    f.Filters.IdMaster == 5 &&
                    f.Filters.IdDetail == 6 &&
                    f.Filters.Activo == true)))
                .Returns(empresas);

            var result = _service.GetAll(true, 5, 6, 2, 25);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información obtenida exitosamente.", result.Message);
            _empresaBMock.Verify(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                f.PageIndex == 2 &&
                f.PageSize == 25 &&
                f.Filters.IdMaster == 5 &&
                f.Filters.IdDetail == 6 &&
                f.Filters.Activo == true)), Times.Once);
            _empresaBMock.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
        }

        [Fact]
        public void GetAll_WhenBusinessThrows_ReturnsFailure()
        {
            _empresaBMock
                .Setup(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()))
                .Throws(new Exception("list error"));

            var result = _service.GetAll(false, 5, 6, 1, 10);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información.", result.Message);
            Assert.Contains("list error", result.Errors);
            _empresaBMock.Verify(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Once);
        }

        [Fact]
        public void Add_WhenCompanyIsValid_InyectsAuditFieldsAndReturnsId()
        {
            var empresa = CreateEmpresa();
            Empresa? capturedEmpresa = null;
            var before = DateTime.UtcNow;

            _empresaBMock
                .Setup(x => x.Add(It.IsAny<Empresa>()))
                .Callback<Empresa>(value => capturedEmpresa = value)
                .Returns(31);

            var result = _service.Add(empresa, 64);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.Equal(31, result.Data);
            Assert.Equal("Empresa agregado exitosamente.", result.Message);
            Assert.NotNull(capturedEmpresa);
            Assert.Equal(64, capturedEmpresa.IdUsuarioCreacion);
            Assert.Equal(64, capturedEmpresa.IdUsuarioModificacion);
            Assert.InRange(capturedEmpresa.FechaCreacion, before, after);
            Assert.InRange(capturedEmpresa.FechaModificacion, before, after);
            _empresaBMock.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Once);
            _empresaBMock.Verify(x => x.Update(It.IsAny<Empresa>()), Times.Never);
        }

        [Fact]
        public void Add_WhenBusinessThrows_ReturnsFailure()
        {
            _empresaBMock
                .Setup(x => x.Add(It.IsAny<Empresa>()))
                .Throws(new Exception("insert error"));

            var result = _service.Add(CreateEmpresa(), 64);

            Assert.False(result.Success);
            Assert.Equal(0, result.Data);
            Assert.Equal("Error al agregar el empresa.", result.Message);
            Assert.Contains("insert error", result.Errors);
            _empresaBMock.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Once);
        }

        [Fact]
        public void Update_WhenCompanyIsValid_InyectsAuditFieldsAndReturnsSuccess()
        {
            var empresa = CreateEmpresa(11);
            Empresa? capturedEmpresa = null;
            var before = DateTime.UtcNow;

            _empresaBMock
                .Setup(x => x.Update(It.IsAny<Empresa>()))
                .Callback<Empresa>(value => capturedEmpresa = value)
                .Returns(true);

            var result = _service.Update(empresa, 70);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Empresa actualizado exitosamente.", result.Message);
            Assert.NotNull(capturedEmpresa);
            Assert.Equal(70, capturedEmpresa.IdUsuarioModificacion);
            Assert.InRange(capturedEmpresa.FechaModificacion, before, after);
            _empresaBMock.Verify(x => x.Update(It.IsAny<Empresa>()), Times.Once);
            _empresaBMock.Verify(x => x.GetCompaniesByUser(It.IsAny<int?>()), Times.Never);
        }

        [Fact]
        public void Update_WhenBusinessThrows_ReturnsFailure()
        {
            _empresaBMock
                .Setup(x => x.Update(It.IsAny<Empresa>()))
                .Throws(new Exception("update error"));

            var result = _service.Update(CreateEmpresa(11), 70);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al actualizar el empresa.", result.Message);
            Assert.Contains("update error", result.Errors);
            _empresaBMock.Verify(x => x.Update(It.IsAny<Empresa>()), Times.Once);
        }

        [Fact]
        public void Delete_WhenBusinessDeletesCompany_ReturnsSuccess()
        {
            _empresaBMock
                .Setup(x => x.Delete(15))
                .Returns(true);

            var result = _service.Delete(15, 70);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Empresa eliminado exitosamente.", result.Message);
            _empresaBMock.Verify(x => x.Delete(15), Times.Once);
            _empresaBMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Delete_WhenBusinessThrows_ReturnsFailure()
        {
            _empresaBMock
                .Setup(x => x.Delete(15))
                .Throws(new Exception("delete error"));

            var result = _service.Delete(15, 70);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al eliminar el empresa.", result.Message);
            Assert.Contains("delete error", result.Errors);
            _empresaBMock.Verify(x => x.Delete(15), Times.Once);
        }

        private static Empresa CreateEmpresa(int id = 1)
        {
            return new Empresa
            {
                Id = id,
                RFC = "AAA010101AAA",
                RazonSocial = "Empresa Demo"
            };
        }
    }
}
