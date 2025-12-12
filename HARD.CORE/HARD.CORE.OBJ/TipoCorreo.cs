using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class TipoCorreo
    {
        public int ClaveTipoCorreo { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public string ClaveUsuarioUltimaActualizacion { get; set; }
        public TipoCorreo() { }
    }

}
