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
    public class PerfilServiceTests
    {
        private readonly Mock<IPerfilB> _perfilBMock;
        private readonly Mock<ILogger<PerfilService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly PerfilService _service;

        public PerfilServiceTests()
        {
            _perfilBMock = new Mock<IPerfilB>();
            _loggerMock = new Mock<ILogger<PerfilService>>();
            _configurationMock = new Mock<IConfiguration>();
            _service = new PerfilService(_loggerMock.Object, _perfilBMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void GetById_WhenProfileExists_ReturnsSuccess()
        {
            var perfil = CreatePerfil(5);

            _perfilBMock
                .Setup(x => x.GetById(5))
                .Returns(perfil);

            var result = _service.GetById(5);

            Assert.True(result.Success);
            Assert.Equal(perfil, result.Data);
            Assert.Equal("Información del perfil obtenida exitosamente.", result.Message);
            _perfilBMock.Verify(x => x.GetById(5), Times.Once);
            _perfilBMock.Verify(x => x.GetUserProfiles(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetById_WhenBusinessThrows_ReturnsFailure()
        {
            _perfilBMock
                .Setup(x => x.GetById(5))
                .Throws(new Exception("get error"));

            var result = _service.GetById(5);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información del perfil.", result.Message);
            Assert.Contains("get error", result.Errors);
            _perfilBMock.Verify(x => x.GetById(5), Times.Once);
        }

        [Fact]
        public void GetAll_WhenProfilesExist_ReturnsSuccess()
        {
            var perfiles = new List<Perfil> { CreatePerfil(1), CreatePerfil(2) };

            _perfilBMock
                .Setup(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                    f.PageIndex == 4 &&
                    f.PageSize == 30 &&
                    f.Filters.Activo == true)))
                .Returns(perfiles);

            var result = _service.GetAll(true, 4, 30);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información de los perfiles obtenida exitosamente.", result.Message);
            _perfilBMock.Verify(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                f.PageIndex == 4 &&
                f.PageSize == 30 &&
                f.Filters.Activo == true)), Times.Once);
            _perfilBMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetAll_WhenBusinessThrows_ReturnsFailure()
        {
            _perfilBMock
                .Setup(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()))
                .Throws(new Exception("list error"));

            var result = _service.GetAll(false, 1, 10);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información de los perfiles.", result.Message);
            Assert.Contains("list error", result.Errors);
            _perfilBMock.Verify(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Once);
        }

        [Fact]
        public void Add_WhenProfileIsValid_InyectsAuditFieldsAndReturnsId()
        {
            var perfil = CreatePerfil();
            Perfil? capturedPerfil = null;
            var before = DateTime.UtcNow;

            _perfilBMock
                .Setup(x => x.Add(It.IsAny<Perfil>()))
                .Callback<Perfil>(value => capturedPerfil = value)
                .Returns(99);

            var result = _service.Add(perfil, 41);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.Equal(99, result.Data);
            Assert.Equal("Perfil agregado exitosamente.", result.Message);
            Assert.NotNull(capturedPerfil);
            Assert.Equal(41, capturedPerfil.IdUsuarioCreacion);
            Assert.Equal(41, capturedPerfil.IdUsuarioModificacion);
            Assert.InRange(capturedPerfil.FechaCreacion, before, after);
            Assert.InRange(capturedPerfil.FechaModificacion, before, after);
            _perfilBMock.Verify(x => x.Add(It.IsAny<Perfil>()), Times.Once);
            _perfilBMock.Verify(x => x.Update(It.IsAny<Perfil>()), Times.Never);
        }

        [Fact]
        public void Add_WhenBusinessThrows_ReturnsFailure()
        {
            _perfilBMock
                .Setup(x => x.Add(It.IsAny<Perfil>()))
                .Throws(new Exception("insert error"));

            var result = _service.Add(CreatePerfil(), 41);

            Assert.False(result.Success);
            Assert.Equal(0, result.Data);
            Assert.Equal("Error al agregar el perfil.", result.Message);
            Assert.Contains("insert error", result.Errors);
            _perfilBMock.Verify(x => x.Add(It.IsAny<Perfil>()), Times.Once);
        }

        [Fact]
        public void Update_WhenProfileIsValid_InyectsAuditFieldsAndReturnsSuccess()
        {
            var perfil = CreatePerfil(18);
            Perfil? capturedPerfil = null;
            var before = DateTime.UtcNow;

            _perfilBMock
                .Setup(x => x.Update(It.IsAny<Perfil>()))
                .Callback<Perfil>(value => capturedPerfil = value)
                .Returns(true);

            var result = _service.Update(perfil, 77);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Perfil actualizado exitosamente.", result.Message);
            Assert.NotNull(capturedPerfil);
            Assert.Equal(77, capturedPerfil.IdUsuarioModificacion);
            Assert.InRange(capturedPerfil.FechaModificacion, before, after);
            _perfilBMock.Verify(x => x.Update(It.IsAny<Perfil>()), Times.Once);
            _perfilBMock.Verify(x => x.Add(It.IsAny<Perfil>()), Times.Never);
        }

        [Fact]
        public void Update_WhenBusinessThrows_ReturnsFailure()
        {
            _perfilBMock
                .Setup(x => x.Update(It.IsAny<Perfil>()))
                .Throws(new Exception("update error"));

            var result = _service.Update(CreatePerfil(18), 77);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al actualizar el perfil.", result.Message);
            Assert.Contains("update error", result.Errors);
            _perfilBMock.Verify(x => x.Update(It.IsAny<Perfil>()), Times.Once);
        }

        [Fact]
        public void Delete_WhenBusinessDeletesProfile_ReturnsSuccess()
        {
            _perfilBMock
                .Setup(x => x.Delete(22))
                .Returns(true);

            var result = _service.Delete(22, 77);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Perfil eliminado exitosamente.", result.Message);
            _perfilBMock.Verify(x => x.Delete(22), Times.Once);
            _perfilBMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Delete_WhenBusinessThrows_ReturnsFailure()
        {
            _perfilBMock
                .Setup(x => x.Delete(22))
                .Throws(new Exception("delete error"));

            var result = _service.Delete(22, 77);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al eliminar el perfil.", result.Message);
            Assert.Contains("delete error", result.Errors);
            _perfilBMock.Verify(x => x.Delete(22), Times.Once);
        }

        [Fact]
        public void GetUserProfiles_WhenProfilesExist_ReturnsSuccess()
        {
            var perfiles = new List<Perfil> { CreatePerfil(10), CreatePerfil(11) };

            _perfilBMock
                .Setup(x => x.GetUserProfiles(12))
                .Returns(perfiles);

            var result = _service.GetUserProfiles(12);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información de los perfiles del usuario obtenida exitosamente.", result.Message);
            _perfilBMock.Verify(x => x.GetUserProfiles(12), Times.Once);
            _perfilBMock.Verify(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Never);
        }

        [Fact]
        public void GetUserProfiles_WhenBusinessThrows_ReturnsFailure()
        {
            _perfilBMock
                .Setup(x => x.GetUserProfiles(12))
                .Throws(new Exception("profiles error"));

            var result = _service.GetUserProfiles(12);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información de los perfiles del usuario.", result.Message);
            Assert.Contains("profiles error", result.Errors);
            _perfilBMock.Verify(x => x.GetUserProfiles(12), Times.Once);
        }

        private static Perfil CreatePerfil(int id = 1)
        {
            return new Perfil
            {
                Id = id,
                Nombre = "Perfil Demo",
                Descripcion = "Perfil de prueba"
            };
        }
    }
}
