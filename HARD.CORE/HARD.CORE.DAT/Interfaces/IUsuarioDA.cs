using HARD.CORE.OBJ;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IUsuarioDA
    {
        Usuario Obtener(string ClaveUsuario);
        List<Usuario> ObtenerTodos();
        List<Usuario> ObtenerUsuariosDirectorioActivo();
        Usuario ObtenerUsuariosDirectorioActivo(string ClaveUsuario);
        List<Usuario> ObtenerUsuariosDirectorioActivoTodos();
        Perfil ObtenerPerfil(string ClaveUsuario);
        DataTable ObtenerActividad();
        DataTable ObtenerDetalleActividad(string claveUsuario);
        bool Desbloquear(string claveUsuario);
        string AutenticarUsuario(string claveUsuario);
        void RegistrarActividad(string claveUsuario, int tipoRegistro);
        bool ActualizandoInformacion();
        void ActualizaIntento(string claveUsuario);
        void ReiniciaIntento(string claveUsuario);
        bool ExisteUsuario(string claveUsuario);
        bool Insertar(Usuario usuario);
        bool Actualizar(Usuario usuario);
    }
}
