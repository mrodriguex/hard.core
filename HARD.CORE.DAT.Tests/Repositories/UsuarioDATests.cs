using HARD.CORE.DAT.Tests.Helpers;
using HARD.CORE.DAT.Repositories;
using HARD.CORE.OBJ;
using Microsoft.EntityFrameworkCore;

namespace HARD.CORE.DAT.Tests.Repositories;

public class UsuarioDATests
{
    [Fact]
    public async Task AddAsync_WhenEntityHasExistingRelations_PersistsAndReturnsId()
    {
        await using var context = TestDataFactory.CreateContext();
        var empresa = new Empresa { Nombre = "Empresa A", Activo = true };
        var perfil = new Perfil { Nombre = "Perfil A", Activo = true };
        context.Empresas.Add(empresa);
        context.Perfiles.Add(perfil);
        await context.SaveChangesAsync();

        var usuario = new Usuario
        {
            ClaveUsuario = "user.test",
            NombreUsuario = "Test",
            Estatus = true,
            Empresas = new List<Empresa> { empresa },
            Perfiles = new List<Perfil> { perfil }
        };

        var repository = new UsuarioDA(context, TestDataFactory.Logger<UsuarioDA>());

        var id = await repository.AddAsync(usuario);

        Assert.True(id > 0);
        var saved = await context.Usuarios
            .Include(u => u.Empresas)
            .Include(u => u.Perfiles)
            .FirstOrDefaultAsync(u => u.Id == id);
        Assert.NotNull(saved);
        Assert.Single(saved!.Empresas);
        Assert.Single(saved.Perfiles);
    }

    [Fact]
    public async Task GetAllAsync_WhenFilteringByStatusAndUsername_ReturnsMatches()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Usuarios.AddRange(
            new Usuario { ClaveUsuario = "admin.one", Estatus = true },
            new Usuario { ClaveUsuario = "guest.one", Estatus = false },
            new Usuario { ClaveUsuario = "admin.two", Estatus = true });
        await context.SaveChangesAsync();

        var repository = new UsuarioDA(context, TestDataFactory.Logger<UsuarioDA>());

        var result = (await repository.GetAllAsync(TestDataFactory.Paged(activo: true, nombre: "admin", pageIndex: 1, pageSize: 10))).ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, u => Assert.True(u.Estatus));
    }

    [Fact]
    public async Task DeleteAsync_WhenEntityDoesNotExist_ReturnsFalse()
    {
        await using var context = TestDataFactory.CreateContext();
        var repository = new UsuarioDA(context, TestDataFactory.Logger<UsuarioDA>());

        var deleted = await repository.DeleteAsync(404);

        Assert.False(deleted);
    }
}
