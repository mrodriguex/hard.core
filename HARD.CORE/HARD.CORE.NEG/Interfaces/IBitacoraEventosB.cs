using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IBitacoraEventosB
    {
        public List<BitacoraEventos> ObtenerTodos(int? claveEvento = null, int? claveTipoEvento = null, int? claveEntidad = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null);
        BitacoraEventos Obtener(int claveBitacoraEventos);
        int Insertar(BitacoraEventos bitacoraEventos);
    }
}
