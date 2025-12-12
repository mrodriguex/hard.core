using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class TipoUsuario
    {
        public TipoUsuario()
        {
        }

        public TipoUsuario(int claveTipoUsuario, string descripcion)
        {
            ClaveTipoUsuario = claveTipoUsuario;
            Descripcion = descripcion;
        }
        public int ClaveTipoUsuario { get; set; }
        public string Descripcion { get; set; }
    }
}
