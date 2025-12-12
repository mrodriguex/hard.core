using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG
{
    public class NivelInglesB : INivelInglesB
    {
        INivelInglesDA _nivelInglesDA;

        public NivelInglesB(INivelInglesDA nivelInglesDA)
        {
            _nivelInglesDA = nivelInglesDA;
        }

        public NivelIngles Obtener(int claveNivelIngles)
        {
            return _nivelInglesDA.Obtener(claveNivelIngles);
        }

        public List<NivelIngles> ObtenerTodos(bool? estatus = null)
        {
            List<NivelIngles> niveles = _nivelInglesDA.ObtenerTodos(estatus: estatus);
            return niveles;
        }

        public int Insertar(NivelIngles nivelIngles)
        {
            return _nivelInglesDA.Insertar(nivelIngles);
        }
        
        public bool Actualizar(NivelIngles nivelIngles)
        {
            return _nivelInglesDA.Actualizar(nivelIngles);
        }

    }
}
