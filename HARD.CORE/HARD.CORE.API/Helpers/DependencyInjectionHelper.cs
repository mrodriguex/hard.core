using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG;
using HARD.CORE.NEG.Interfaces;

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
        services.AddScoped<ILdapAuthentication, LdapAuthentication>();
        services.AddScoped<IUsuarioDA, UsuarioDA>();
        services.AddScoped<ICorreoDA, CorreoDA>();
        services.AddScoped<ISugerenciaDA, SugerenciaDA>();
        services.AddScoped<IPerfilDA, PerfilDA>();
        services.AddScoped<IEmpresaDA, EmpresaDA>();
        services.AddScoped<IMenuDA, MenuDA>();
        services.AddScoped<IArchivosDA, ArchivosDA>();
        services.AddScoped<IArchivosDescargasDA, ArchivosDescargasDA>();
        services.AddScoped<IAvisoDA, AvisoDA>();
        services.AddScoped<IBitacoraEventosDA, BitacoraEventosDA>();
        services.AddScoped<IEmpresaDA, EmpresaDA>();
        services.AddScoped<INivelMinimoEstudiosDA, NivelMinimoEstudiosDA>();
        services.AddScoped<ILdapAuthentication, LdapAuthentication>();
        services.AddScoped<IMenuDA, MenuDA>();
        services.AddScoped<IMotivoVacanteDA, MotivoVacanteDA>();
        services.AddScoped<INivelInglesDA, NivelInglesDA>();
        services.AddScoped<INotificacionDA, NotificacionDA>();
        services.AddScoped<IPerfilDA, PerfilDA>();
        services.AddScoped<IRutasDA, RutasDA>();
        services.AddScoped<ISugerenciaDA, SugerenciaDA>();
        services.AddScoped<IUsuarioDA, UsuarioDA>();
        services.AddScoped<ISeguridadAccionDA, SeguridadAccionDA>();
        services.AddScoped<IFlujoAutorizacionDA, FlujoAutorizacionDA>();
        services.AddScoped<ITipoCorreoDA, TipoCorreoDA>();
        services.AddScoped<ICorreoVariableDA, CorreoVariableDA>();
        services.AddScoped<IHerenciaPerfilDA, HerenciaPerfilDA>();

        // Register services B
        services.AddScoped<IArchivoB, ArchivoB>();
        services.AddScoped<IAvisoB, AvisoB>();
        services.AddScoped<IBitacoraB, BitacoraB>();
        services.AddScoped<ICorreoB, CorreoB>();
        services.AddScoped<ICryptographer, Cryptographer>();
        services.AddScoped<IEmpresaB, EmpresaB>();
        services.AddScoped<IMenuB, MenuB>();
        services.AddScoped<IBitacoraEventosB, BitacoraEventosB>();
        services.AddScoped<IAvisoB, AvisoB>();
        services.AddScoped<INivelMinimoEstudiosB, NivelMinimoEstudiosB>();
        services.AddScoped<IMotivoVacanteB, MotivoVacanteB>();
        services.AddScoped<INivelInglesB, NivelInglesB>();
        services.AddScoped<INotificacionB, NotificacionB>();
        services.AddScoped<IPerfilB, PerfilB>();
        services.AddScoped<ISugerenciaB, SugerenciaB>();
        services.AddScoped<IUsuarioB, UsuarioB>();
        services.AddScoped<ISeguridadAccionB, SeguridadAccionB>();
        services.AddScoped<IFlujoAutorizacionB, FlujoAutorizacionB>();
        services.AddScoped<ITipoCorreoB, TipoCorreoB>();
        services.AddScoped<ICorreoVariableB, CorreoVariableB>();
        services.AddScoped<IHerenciaPerfilB, HerenciaPerfilB>();

        return services;
    }
}