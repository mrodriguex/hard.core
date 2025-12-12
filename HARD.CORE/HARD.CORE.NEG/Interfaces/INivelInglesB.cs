using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    public interface INivelInglesB
    {
        
        NivelIngles Obtener(int claveNivelIngles);
        List<NivelIngles> ObtenerTodos(bool? estatus = null);        
        int Insertar(NivelIngles nivelIngles);
        bool Actualizar(NivelIngles nivelIngles);
        
    }
}
