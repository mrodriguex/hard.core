using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IHerenciaPerfilB
    {
        List<HerenciaPerfil> ObtenerTodos(bool? estatus = null);
        bool Existe(HerenciaPerfil perfil);
        int Insertar(HerenciaPerfil perfil);
        bool Actualizar(HerenciaPerfil perfil);
    }
}
