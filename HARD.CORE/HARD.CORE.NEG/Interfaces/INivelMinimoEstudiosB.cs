using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    public interface INivelMinimoEstudiosB
    {
        NivelMinimoEstudios Obtener(int claveNivelMinimoEstudios);
        List<NivelMinimoEstudios> ObtenerTodos(bool? estatus = null);
        int Insertar(NivelMinimoEstudios nivelMinimoEstudios);
        bool Actualizar(NivelMinimoEstudios nivelMinimoEstudios);
        
    }
}
