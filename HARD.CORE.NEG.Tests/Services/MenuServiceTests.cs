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
    public class MenuServiceTests
    {
        private readonly Mock<IMenuB> _menuBMock;
        private readonly Mock<ILogger<MenuService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly MenuService _service;

        public MenuServiceTests()
        {
            _menuBMock = new Mock<IMenuB>();
            _loggerMock = new Mock<ILogger<MenuService>>();
            _configurationMock = new Mock<IConfiguration>();
            _service = new MenuService(_loggerMock.Object, _menuBMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void GetById_WhenMenuExists_ReturnsSuccess()
        {
            var menu = CreateMenu(2);

            _menuBMock
                .Setup(x => x.GetById(2))
                .Returns(menu);

            var result = _service.GetById(2);

            Assert.True(result.Success);
            Assert.Equal(menu, result.Data);
            Assert.Equal("Información del menu obtenida exitosamente.", result.Message);
            _menuBMock.Verify(x => x.GetById(2), Times.Once);
            _menuBMock.Verify(x => x.GetMenusByProfile(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetById_WhenBusinessThrows_ReturnsFailure()
        {
            _menuBMock
                .Setup(x => x.GetById(2))
                .Throws(new Exception("get error"));

            var result = _service.GetById(2);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información del menu.", result.Message);
            Assert.Contains("get error", result.Errors);
            _menuBMock.Verify(x => x.GetById(2), Times.Once);
        }

        [Fact]
        public void GetAll_WhenMenusExist_ReturnsSuccess()
        {
            var menus = new List<Menu> { CreateMenu(1), CreateMenu(2) };

            _menuBMock
                .Setup(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                    f.PageIndex == 2 &&
                    f.PageSize == 50 &&
                    f.Filters.Activo == true)))
                .Returns(menus);

            var result = _service.GetAll(true, 2, 50);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información de los menues obtenida exitosamente.", result.Message);
            _menuBMock.Verify(x => x.GetAll(It.Is<global::PagedFilter<BaseFilter>>(f =>
                f.PageIndex == 2 &&
                f.PageSize == 50 &&
                f.Filters.Activo == true)), Times.Once);
            _menuBMock.Verify(x => x.Add(It.IsAny<Menu>()), Times.Never);
        }

        [Fact]
        public void GetAll_WhenBusinessThrows_ReturnsFailure()
        {
            _menuBMock
                .Setup(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()))
                .Throws(new Exception("list error"));

            var result = _service.GetAll(false, 1, 5);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información de los menues.", result.Message);
            Assert.Contains("list error", result.Errors);
            _menuBMock.Verify(x => x.GetAll(It.IsAny<global::PagedFilter<BaseFilter>>()), Times.Once);
        }

        [Fact]
        public void Add_WhenMenuIsValid_InyectsAuditFieldsAndReturnsId()
        {
            var menu = CreateMenu();
            Menu? capturedMenu = null;
            var before = DateTime.UtcNow;

            _menuBMock
                .Setup(x => x.Add(It.IsAny<Menu>()))
                .Callback<Menu>(value => capturedMenu = value)
                .Returns(14);

            var result = _service.Add(menu, 61);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.Equal(14, result.Data);
            Assert.Equal("Menu agregado exitosamente.", result.Message);
            Assert.NotNull(capturedMenu);
            Assert.Equal(61, capturedMenu.IdUsuarioCreacion);
            Assert.Equal(61, capturedMenu.IdUsuarioModificacion);
            Assert.InRange(capturedMenu.FechaCreacion, before, after);
            Assert.InRange(capturedMenu.FechaModificacion, before, after);
            _menuBMock.Verify(x => x.Add(It.IsAny<Menu>()), Times.Once);
            _menuBMock.Verify(x => x.Update(It.IsAny<Menu>()), Times.Never);
        }

        [Fact]
        public void Add_WhenBusinessThrows_ReturnsFailure()
        {
            _menuBMock
                .Setup(x => x.Add(It.IsAny<Menu>()))
                .Throws(new Exception("insert error"));

            var result = _service.Add(CreateMenu(), 61);

            Assert.False(result.Success);
            Assert.Equal(0, result.Data);
            Assert.Equal("Error al agregar el menu.", result.Message);
            Assert.Contains("insert error", result.Errors);
            _menuBMock.Verify(x => x.Add(It.IsAny<Menu>()), Times.Once);
        }

        [Fact]
        public void Update_WhenMenuIsValid_InyectsAuditFieldsAndReturnsSuccess()
        {
            var menu = CreateMenu(19);
            Menu? capturedMenu = null;
            var before = DateTime.UtcNow;

            _menuBMock
                .Setup(x => x.Update(It.IsAny<Menu>()))
                .Callback<Menu>(value => capturedMenu = value)
                .Returns(true);

            var result = _service.Update(menu, 72);
            var after = DateTime.UtcNow;

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Menu actualizado exitosamente.", result.Message);
            Assert.NotNull(capturedMenu);
            Assert.Equal(72, capturedMenu.IdUsuarioModificacion);
            Assert.InRange(capturedMenu.FechaModificacion, before, after);
            _menuBMock.Verify(x => x.Update(It.IsAny<Menu>()), Times.Once);
            _menuBMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Update_WhenBusinessThrows_ReturnsFailure()
        {
            _menuBMock
                .Setup(x => x.Update(It.IsAny<Menu>()))
                .Throws(new Exception("update error"));

            var result = _service.Update(CreateMenu(19), 72);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al actualizar el menu.", result.Message);
            Assert.Contains("update error", result.Errors);
            _menuBMock.Verify(x => x.Update(It.IsAny<Menu>()), Times.Once);
        }

        [Fact]
        public void Delete_WhenBusinessDeletesMenu_ReturnsSuccess()
        {
            _menuBMock
                .Setup(x => x.Delete(25))
                .Returns(true);

            var result = _service.Delete(25, 72);

            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Menu eliminado exitosamente.", result.Message);
            _menuBMock.Verify(x => x.Delete(25), Times.Once);
            _menuBMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Delete_WhenBusinessThrows_ReturnsFailure()
        {
            _menuBMock
                .Setup(x => x.Delete(25))
                .Throws(new Exception("delete error"));

            var result = _service.Delete(25, 72);

            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Error al eliminar el menu.", result.Message);
            Assert.Contains("delete error", result.Errors);
            _menuBMock.Verify(x => x.Delete(25), Times.Once);
        }

        [Fact]
        public void GetMenusByUser_WhenMenusExist_ReturnsSuccess()
        {
            var menus = new List<Menu> { CreateMenu(31), CreateMenu(32) };

            _menuBMock
                .Setup(x => x.GetMenusByUser(8, 9))
                .Returns(menus);

            var result = _service.GetMenusByUser(8, 9);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información de los menues del usuario obtenida exitosamente.", result.Message);
            _menuBMock.Verify(x => x.GetMenusByUser(8, 9), Times.Once);
            _menuBMock.Verify(x => x.GetMenusByProfile(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetMenusByUser_WhenBusinessThrows_ReturnsFailure()
        {
            _menuBMock
                .Setup(x => x.GetMenusByUser(8, 9))
                .Throws(new Exception("user menus error"));

            var result = _service.GetMenusByUser(8, 9);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información de los menues del usuario.", result.Message);
            Assert.Contains("user menus error", result.Errors);
            _menuBMock.Verify(x => x.GetMenusByUser(8, 9), Times.Once);
        }

        [Fact]
        public void GetMenusByProfile_WhenMenusExist_ReturnsSuccess()
        {
            var menus = new List<Menu> { CreateMenu(41), CreateMenu(42) };

            _menuBMock
                .Setup(x => x.GetMenusByProfile(3))
                .Returns(menus);

            var result = _service.GetMenusByProfile(3);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Información de los menues del perfil obtenida exitosamente.", result.Message);
            _menuBMock.Verify(x => x.GetMenusByProfile(3), Times.Once);
            _menuBMock.Verify(x => x.GetMenusByUser(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetMenusByProfile_WhenBusinessThrows_ReturnsFailure()
        {
            _menuBMock
                .Setup(x => x.GetMenusByProfile(3))
                .Throws(new Exception("profile menus error"));

            var result = _service.GetMenusByProfile(3);

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Error al obtener la información de los menues del perfil.", result.Message);
            Assert.Contains("profile menus error", result.Errors);
            _menuBMock.Verify(x => x.GetMenusByProfile(3), Times.Once);
        }

        private static Menu CreateMenu(int id = 1)
        {
            return new Menu
            {
                Id = id,
                Nombre = "Menu Demo",
                Ruta = "/demo",
                Imagen = "demo.png"
            };
        }
    }
}
