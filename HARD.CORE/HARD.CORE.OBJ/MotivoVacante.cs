using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class MotivoVacante
    {
        public int ClaveMotivoVacante { get; set; }
        public string Descripcion { get; set; }
        public bool Sustituye { get; set; }
        public bool? CentroCostos { get; set; }
        public bool? MotivoIncapacidad { get; set; }
        public bool? NumeroMeses { get; set; }
        public bool? JustificacionNuevoPuesto { get; set; }
        public bool Estatus { get; set; }
        public string ClaveUsuarioUltimaActualizacion { get; set; }
        public MotivoVacante()
        {

        }
    }
}
