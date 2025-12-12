using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface ISeguridadAccionDA
    {
        List<SeguridadAccion> ObtenerTodos(int ClavePerfiL);
        SeguridadAccion Obtener(int ClavePerfil, int ClaveMenu);
        bool Actualizar(List<SeguridadAccion> seguridadAccion);
    }
}
