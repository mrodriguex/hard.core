using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IRutasDA
    {
        List<Rutas> ObtenerTodos(bool? estatus = null);
        Rutas Obtener(int claveRuta);
        int Insertar(Rutas ruta);
        bool Actualizar(Rutas ruta);
        bool Eliminar(Rutas ruta);
    }
}
