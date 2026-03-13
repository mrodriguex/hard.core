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
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioB> _usuarioBMock;
        private readonly Mock<ILogger<UsuarioService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _usuarioBMock = new Mock<IUsuarioB>();
            _loggerMock = new Mock<ILogger<UsuarioService>>();
            _configurationMock = new Mock<IConfiguration>();
            _service = new UsuarioService(_loggerMock.Object, _usuarioBMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void GetById_WhenUserExists_ReturnsSuccessfulResult()
        {
            var usuario = CreateUsuario(10);

            _usuarioBMock
                .Setup(x => x.GetById(10))
                .Returns(usuario);

            var result = _service.GetById(10);

            Assert.True(result.Success);
            Assert.Equal(usuario, result.Data);
            Assert.Equal("Información del usuario obtenida exitosamente.", result.Message);
            Assert.Empty(result.Errors);
            _usuarioBMock.Verify(x => x.GetById(10), Times.Once);
            _usuarioBMock.Verify(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Never);
        }

        [Fact]
        public void GetById_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioBMock
                .Setup(x => x.GetById(10))
                .Throws(new InvalidOperationException("db error"));

            var result = _service.GetById(10);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información del usuario.", result.Message);
            Assert.Contains("db error", result.Errors);
            _usuarioBMock.Verify(x => x.GetById(10), Times.Once);
        }

        [Fact]
        public void GetAll_WhenUsersExist_ReturnsSuccessfulResult()
        {
            var usuarios = new List<Usuario> { CreateUsuario(1), CreateUsuario(2) };

            _usuarioBMock
                .Setup(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                    f.PageIndex == 2 &&
                    f.PageSize == 5 &&
                    f.Filters.Activo == true)))
                .Returns(usuarios);

            var result = _service.GetAll(true, 2, 5);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información de los usuarios obtenida exitosamente.", result.Message);
            _usuarioBMock.Verify(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                f.PageIndex == 2 &&
                f.PageSize == 5 &&
                f.Filters.Activo == true)), Times.Once);
            _usuarioBMock.Verify(x => x.Add(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void GetAll_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioBMock
                .Setup(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()))
                .Throws(new Exception("query failed"));

            var result = _service.GetAll(false, 1, 20);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información de los usuarios.", result.Message);
            Assert.Contains("query failed", result.Errors);
            _usuarioBMock.Verify(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Once);
        }

        [Fact]
        public void Add_WhenUserIsValid_InyectsAuditFieldsAndReturnsId()
        {
            var usuario = CreateUsuario();
            Usuario? capturedUsuario = null;
            var before = DateTime.UtcNow;

            _usuarioBMock
                .Setup(x => x.Add(It.IsAny<Usuario>()))
                .Callback<Usuario>(value => capturedUsuario = value)
                .Returns(55);

            var result = _service.Add(usuario, 99);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.Equal(55, result.Data);
            Assert.Equal("Usuario agregado exitosamente.", result.Message);
            Assert.NotNull(capturedUsuario);
            Assert.Equal(99, capturedUsuario.IdUsuarioCreacion);
            Assert.Equal(99, capturedUsuario.IdUsuarioModificacion);
            Assert.InRange(capturedUsuario.FechaCreacion, before, after);
            Assert.InRange(capturedUsuario.FechaModificacion, before, after);
            _usuarioBMock.Verify(x => x.Add(It.IsAny<Usuario>()), Times.Once);
            _usuarioBMock.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void Add_WhenBusinessThrows_ReturnsFailure()
        {
            var usuario = CreateUsuario();

            _usuarioBMock
                .Setup(x => x.Add(It.IsAny<Usuario>()))
                .Throws(new Exception("insert error"));

            var result = _service.Add(usuario, 77);

            Assert.False(result.Success);
            Assert.Equal(0, result.Data);
            Assert.Equal("Error al agregar el usuario.", result.Message);
            Assert.Contains("insert error", result.Errors);
            _usuarioBMock.Verify(x => x.Add(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public void Update_WhenUserIsValid_InyectsAuditFieldsAndReturnsSuccess()
        {
            var usuario = CreateUsuario(8);
            Usuario? capturedUsuario = null;
            var before = DateTime.UtcNow;

            _usuarioBMock
                .Setup(x => x.Update(It.IsAny<Usuario>()))
                .Callback<Usuario>(value => capturedUsuario = value)
                .Returns(true);

            var result = _service.Update(usuario, 44);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Usuario actualizado exitosamente.", result.Message);
            Assert.NotNull(capturedUsuario);
            Assert.Equal(44, capturedUsuario.IdUsuarioModificacion);
            Assert.InRange(capturedUsuario.FechaModificacion, before, after);
            _usuarioBMock.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Once);
            _usuarioBMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Update_WhenBusinessThrows_ReturnsFailure()
        {
            var usuario = CreateUsuario(8);

            _usuarioBMock
                .Setup(x => x.Update(It.IsAny<Usuario>()))
                .Throws(new Exception("update error"));

            var result = _service.Update(usuario, 44);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al actualizar el usuario.", result.Message);
            Assert.Contains("update error", result.Errors);
            _usuarioBMock.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public void Delete_WhenBusinessDeletesUser_ReturnsSuccess()
        {
            _usuarioBMock
                .Setup(x => x.Delete(13))
                .Returns(true);

            var result = _service.Delete(13, 7);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Usuario eliminado exitosamente.", result.Message);
            _usuarioBMock.Verify(x => x.Delete(13), Times.Once);
            _usuarioBMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Delete_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioBMock
                .Setup(x => x.Delete(13))
                .Throws(new Exception("delete error"));

            var result = _service.Delete(13, 7);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al eliminar el usuario.", result.Message);
            Assert.Contains("delete error", result.Errors);
            _usuarioBMock.Verify(x => x.Delete(13), Times.Once);
        }

        [Fact]
        public void Exists_WhenUserExists_ReturnsSuccess()
        {
            _usuarioBMock
                .Setup(x => x.Exists(13))
                .Returns(true);

            var result = _service.Exists(13);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Verificación de existencia del usuario realizada exitosamente.", result.Message);
            _usuarioBMock.Verify(x => x.Exists(13), Times.Once);
            _usuarioBMock.Verify(x => x.GetByUsername(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Exists_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioBMock
                .Setup(x => x.Exists(13))
                .Throws(new Exception("exists error"));

            var result = _service.Exists(13);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al verificar la existencia del usuario.", result.Message);
            Assert.Contains("exists error", result.Errors);
            _usuarioBMock.Verify(x => x.Exists(13), Times.Once);
        }

        private static Usuario CreateUsuario(int id = 1)
        {
            return new Usuario
            {
                Id = id,
                ClaveUsuario = "usuario.test",
                NombreUsuario = "Usuario",
                ApellidoPaterno = "Prueba",
                ApellidoMaterno = "Demo",
                Correo = "usuario@test.com"
            };
        }
    }
}
