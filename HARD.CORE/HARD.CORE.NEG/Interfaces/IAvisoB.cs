using System.Collections.Generic;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IAvisoB
    {
        Aviso Obtener(int claveAviso);
        List<Aviso> ObtenerTodos(bool? estatus = null);
        List<Aviso> ObtenerActivosLista();
        bool PuedeInsertarAviso();
        int Insertar(Aviso aviso);
        bool Actualizar(Aviso aviso);
        bool Eliminar(int ClaveAviso, string ClaveUsuarioActualizacion);
    }
}