using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class FlujoAutorizacion
    {
        public int ClaveFlujoAutorizacion { get; set; }
        public int ClaveEstatus { get; set; }
        public string CodigoPuesto { get; set; }
        public string Puesto { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public bool Estatus { get; set; }
        public string ClaveUsuarioAlta { get; set; }
        public FlujoAutorizacion() { }
    }
}
