using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface INivelInglesDA
    {
        List<NivelIngles> ObtenerTodos(bool? estatus = null);
        NivelIngles Obtener(int ClaveNivelIngles);
        int Insertar(NivelIngles nivelIngles);
        bool Actualizar(NivelIngles nivelIngles);

    }
}
