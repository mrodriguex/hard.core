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
    public class NivelMinimoEstudiosB : INivelMinimoEstudiosB
    {

        private INivelMinimoEstudiosDA _nivelMinimoEstudiosDA;

        /// <summary>
        /// Initializes a new instance of the <see cref="NivelMinimoEstudiosB"/> class.
        /// </summary>
        /// <param name="nivelMinimoEstudiosDA">The nivel minimo estudios data access layer.</param>
        public NivelMinimoEstudiosB(INivelMinimoEstudiosDA nivelMinimoEstudiosDA)
        {
            _nivelMinimoEstudiosDA = nivelMinimoEstudiosDA;
        }

        public NivelMinimoEstudios Obtener(int claveNivelMinimoEstudios)
        {
            return _nivelMinimoEstudiosDA.Obtener(claveNivelMinimoEstudios: claveNivelMinimoEstudios);
        }

        public bool Actualizar(NivelMinimoEstudios nivelMinimoEstudios)
        {
            return _nivelMinimoEstudiosDA.Actualizar(nivelMinimoEstudios: nivelMinimoEstudios);
        }

        public int Insertar(NivelMinimoEstudios nivelMinimoEstudios)
        {
            return _nivelMinimoEstudiosDA.Insertar(nivelMinimoEstudios: nivelMinimoEstudios);
        }

        public List<NivelMinimoEstudios> ObtenerTodos(bool? estatus)
        {
            return _nivelMinimoEstudiosDA.ObtenerTodos(estatus: estatus);
        }
    }
}
