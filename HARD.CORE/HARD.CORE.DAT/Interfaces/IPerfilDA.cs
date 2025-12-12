using HARD.CORE.OBJ;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IPerfilDA
    {
        List<Perfil> ObtenerTodos(int? clavePerfil = null, bool? estatus = null);
        Perfil Obtener(int clavePerfil, bool? estatus = null);
        List<Perfil> ObtenerPerfilesUsuario(string claveUsuario);
        int Insertar(Perfil perfil);
        bool Actualizar(Perfil perfil);
        bool InsertarPerfilUsuario(Perfil perfil, string ClaveUsuario);
        bool EliminarPerfilUsuario(string ClaveUsuario);
        bool ConfigurarMenu_Perfil(Perfil perfil);
    }
}
