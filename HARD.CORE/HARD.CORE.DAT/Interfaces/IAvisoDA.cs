using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IAvisoDA
    {
        public List<Aviso> ObtenerTodos(bool? estatus);
        Aviso Obtener(int claveAviso);
        List<Aviso> ObtenerActivosLista();
        int Insertar(Aviso aviso);
        bool Actualizar(Aviso aviso);
        bool Eliminar(Aviso aviso);
    }
}
