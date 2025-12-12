using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface INivelMinimoEstudiosDA
    {
        List<NivelMinimoEstudios> ObtenerTodos(bool? estatus = null);
        NivelMinimoEstudios Obtener(int claveNivelMinimoEstudios);
        int Insertar(NivelMinimoEstudios nivelMinimoEstudios);
        public bool Actualizar(NivelMinimoEstudios nivelMinimoEstudios);
    }
}
