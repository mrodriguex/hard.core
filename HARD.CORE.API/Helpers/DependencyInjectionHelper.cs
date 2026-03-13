using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.NEG.Services;
using HARD.CORE.OBJ;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    /// <summary>
    /// Adds application services to the DI container.
    /// </summary>
    /// <param name="services">
    /// The service collection to add services to.
    /// </param>
    /// <param name="configuration">
    /// The configuration to use.
    /// </param>
    /// <returns>
    /// The service collection.
    /// </returns>      
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register configuration
        services.AddSingleton<IConfiguration>(configuration);


        // Register services DA
        services.AddScoped<IRepositoryBase<Usuario, BaseFilter, int>, UsuarioDA>();
        services.AddScoped<IRepositoryBase<Perfil, BaseFilter, int>, PerfilDA>();
        services.AddScoped<IRepositoryBase<Cliente, BaseFilter, int>, ClienteDA>();
        services.AddScoped<IRepositoryBase<Menu, BaseFilter, int>, MenuDA>();
        services.AddScoped<IRepositoryBase<Empresa, BaseFilter, int>, EmpresaDA>();
        services.AddScoped<IRepositoryBase<Menu, BaseFilter, int>, MenuDA>();
        services.AddScoped<IRepositoryBase<Perfil, BaseFilter, int>, PerfilDA>();
        services.AddScoped<IRepositoryBase<Usuario, BaseFilter, int>, UsuarioDA>();
        services.AddScoped<IRepositoryBase<Cliente, BaseFilter, int>, ClienteDA>();

        // Register services B
        services.AddScoped<ICryptographerB, CryptographerSHA512B>();
        services.AddScoped<ICryptographerService, CryptographerService>();

        services.AddScoped<IClienteB, ClienteB>();
        services.AddScoped<IEmpresaB, EmpresaB>();
        services.AddScoped<IMenuB, MenuB>();
        services.AddScoped<IPerfilB, PerfilB>();
        services.AddScoped<IUsuarioB, UsuarioB>();

        services.AddScoped<ClienteService>();
        services.AddScoped<EmpresaService>();
        services.AddScoped<MenuService>();
        services.AddScoped<PerfilService>();
        services.AddScoped<UsuarioService>();
        services.AddScoped<ConfigService>();

        // Register DbContext
        services.AddDbContext<HardCoreDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("HARD.CORE.DAT")
            ));
        
        return services;
    }
}