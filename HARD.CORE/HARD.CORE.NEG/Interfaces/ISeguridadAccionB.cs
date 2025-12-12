using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG.Interfaces
{
    public interface ISeguridadAccionB
    {
        List<SeguridadAccion> ObtenerTodos(int clavePerfil);
        SeguridadAccion Obtener(int clavePerfil, int claveMenu);
        bool Actualizar(List<SeguridadAccion> seguridadAccion);
    }
}
