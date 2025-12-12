using HARD.CORE.OBJ;

using HARD.CORE.NEG.Interfaces;
using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    public class TipoCorreoB : ITipoCorreoB
    {
        private readonly ITipoCorreoDA _tipoCorreoDA;

        public TipoCorreoB(ITipoCorreoDA tipoCorreoDA)
        {
            _tipoCorreoDA = tipoCorreoDA;
        }

        public TipoCorreo Obtener(int claveTipoCorreo)
        {
            return _tipoCorreoDA.Obtener(claveTipoCorreo);
        }

        public List<TipoCorreo> ObtenerTodos(bool? estatus = false)
        {
            return _tipoCorreoDA.ObtenerTodos(estatus: estatus);
        }

    }
}
