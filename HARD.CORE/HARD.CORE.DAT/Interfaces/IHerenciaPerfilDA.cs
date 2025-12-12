using HARD.CORE.OBJ;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IHerenciaPerfilDA
    {
        List<HerenciaPerfil> ObtenerTodos(bool? estatus = null);
        bool Existe(HerenciaPerfil herenciaPerfil);
        int Insertar(HerenciaPerfil herenciaPerfil);
        bool Actualizar(HerenciaPerfil herenciaPerfil);
    }
}
