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
        private readonly Mock<HARD.CORE.DAT.Interfaces.IRepositoryBase<Usuario, BaseFilter, int>> _usuarioRepositoryMock;
        private readonly Mock<HARD.CORE.NEG.Interfaces.ICryptographerB> _cryptographerMock;
        private readonly Mock<ILogger<UsuarioService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<HARD.CORE.DAT.Interfaces.IRepositoryBase<Usuario, BaseFilter, int>>();
            _cryptographerMock = new Mock<HARD.CORE.NEG.Interfaces.ICryptographerB>();
            _loggerMock = new Mock<ILogger<UsuarioService>>();
            _configurationMock = new Mock<IConfiguration>();
            _service = new UsuarioService(_loggerMock.Object, _usuarioRepositoryMock.Object, _cryptographerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task GetById_WhenUserExists_ReturnsSuccessfulResult()
        {
            var usuario = CreateUsuario(10);
            _usuarioRepositoryMock
                .Setup(x => x.GetByIdAsync(10))
                .ReturnsAsync(usuario);

            var result = await _service.GetByIdAsync(10);

            Assert.True(result.Success);
            Assert.Equal(usuario, result.Data);
            Assert.Equal("Información del usuario obtenida exitosamente.", result.Message);
            Assert.Empty(result.Errors);
            _usuarioRepositoryMock.Verify(x => x.GetByIdAsync(10), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioRepositoryMock
                .Setup(x => x.GetByIdAsync(10))
                .Throws(new InvalidOperationException("db error"));

            var result = await _service.GetByIdAsync(10);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información del usuario.", result.Message);
            Assert.Contains("db error", result.Errors);
            _usuarioRepositoryMock.Verify(x => x.GetByIdAsync(10), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenUsersExist_ReturnsSuccessfulResult()
        {
            var usuarios = new List<Usuario> { CreateUsuario(1), CreateUsuario(2) };

            _usuarioRepositoryMock
                .Setup(x => x.GetAllAsync(It.Is<global::PagedFilter<BaseFilter>>(f =>
                    f.PageIndex == 2 &&
                    f.PageSize == 5 &&
                    f.Filters.Activo == true)))
                .ReturnsAsync(usuarios);

            var result = await _service.GetAllAsync(true, 2, 5);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.ToList().Count);
            Assert.Equal("Información obtenida exitosamente.", result.Message);
            _usuarioRepositoryMock.Verify(x => x.GetAllAsync(It.Is<global::PagedFilter<BaseFilter>>(f =>
                f.PageIndex == 2 &&
                f.PageSize == 5 &&
                f.Filters.Activo == true)), Times.Once);
            _usuarioRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task GetAll_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioRepositoryMock
                .Setup(x => x.GetAllAsync(It.IsAny<global::PagedFilter<BaseFilter>>()))
                .Throws(new Exception("query failed"));

            var result = await _service.GetAllAsync(false, 1, 20);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información de los usuarios.", result.Message);
            Assert.Contains("query failed", result.Errors);
            _usuarioRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Once);
        }

        [Fact]
        public async Task Add_WhenUserIsValid_InyectsAuditFieldsAndReturnsId()
        {
            var usuario = CreateUsuario();
            Usuario? capturedUsuario = null;
            var before = DateTime.UtcNow;

            _usuarioRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Usuario>()))
                .Callback<Usuario>(value => capturedUsuario = value)
                .ReturnsAsync(55);

            var result = await _service.AddAsync(usuario, 99);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.Equal(55, result.Data);
            Assert.Equal("Usuario agregado exitosamente.", result.Message);
            Assert.NotNull(capturedUsuario);
            Assert.Equal(99, capturedUsuario.IdUsuarioCreacion);
            Assert.Equal(99, capturedUsuario.IdUsuarioModificacion);
            Assert.InRange(capturedUsuario.FechaCreacion, before, after);
            Assert.InRange(capturedUsuario.FechaModificacion, before, after);
            _usuarioRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Once);
            _usuarioRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task Add_WhenBusinessThrows_ReturnsFailure()
        {
            var usuario = CreateUsuario();

            _usuarioRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Usuario>()))
                .ThrowsAsync(new Exception("insert error"));

            var result = await _service.AddAsync(usuario, 77);

            Assert.False(result.Success);
            Assert.Equal(0, result.Data);
            Assert.Equal("Error al agregar el usuario.", result.Message);
            Assert.Contains("insert error", result.Errors);
            _usuarioRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task Update_WhenUserIsValid_InyectsAuditFieldsAndReturnsSuccess()
        {
            var usuario = CreateUsuario(8);
            Usuario? capturedUsuario = null;
            var before = DateTime.UtcNow;

            _usuarioRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Usuario>()))
                .Callback<Usuario>(value => capturedUsuario = value)
                .ReturnsAsync(true);

            var result = await _service.UpdateAsync(usuario, 44);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Usuario actualizado exitosamente.", result.Message);
            Assert.NotNull(capturedUsuario);
            Assert.Equal(44, capturedUsuario.IdUsuarioModificacion);
            Assert.InRange(capturedUsuario.FechaModificacion, before, after);
            _usuarioRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
            _usuarioRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Update_WhenBusinessThrows_ReturnsFailure()
        {
            var usuario = CreateUsuario(8);

            _usuarioRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Usuario>()))
                .ThrowsAsync(new Exception("update error"));

            var result = await _service.UpdateAsync(usuario, 44);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al actualizar el usuario.", result.Message);
            Assert.Contains("update error", result.Errors);
            _usuarioRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenBusinessDeletesUser_ReturnsSuccess()
        {
            _usuarioRepositoryMock
                .Setup(x => x.DeleteAsync(13))
                .ReturnsAsync(true);

            var result = await _service.DeleteAsync(13, 7);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Usuario eliminado exitosamente.", result.Message);
            _usuarioRepositoryMock.Verify(x => x.DeleteAsync(13), Times.Once);
            _usuarioRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Delete_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioRepositoryMock
                .Setup(x => x.DeleteAsync(13))
                .ThrowsAsync(new Exception("delete error"));

            var result = await _service.DeleteAsync(13, 7);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al eliminar el usuario.", result.Message);
            Assert.Contains("delete error", result.Errors);
            _usuarioRepositoryMock.Verify(x => x.DeleteAsync(13), Times.Once);
        }

        [Fact]
        public async Task Exists_WhenUserExists_ReturnsSuccess()
        {
            var usuario = CreateUsuario(13);
            _usuarioRepositoryMock
                .Setup(x => x.GetByIdAsync(13))
                .ReturnsAsync(usuario);

            var result = await _service.ExistsAsync(13);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Verificación de existencia del usuario realizada exitosamente.", result.Message);
            _usuarioRepositoryMock.Verify(x => x.GetByIdAsync(13), Times.Once);
        }

        [Fact]
        public async Task Exists_WhenBusinessThrows_ReturnsFailure()
        {
            _usuarioRepositoryMock
                .Setup(x => x.GetByIdAsync(13))
                .ThrowsAsync(new Exception("exists error"));

            var result = await _service.ExistsAsync(13);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al verificar la existencia del usuario.", result.Message);
            Assert.Contains("exists error", result.Errors);
            _usuarioRepositoryMock.Verify(x => x.GetByIdAsync(13), Times.Once);
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
