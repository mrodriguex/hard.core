using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class PerfilUsuario
    {
        public string ClaveUsuario { get; set; }
        public int ClavePerfil { get; set; }
        public PerfilUsuario() { }
        public PerfilUsuario(string claveUsuario, int clavePerfil)
        {
            ClaveUsuario = claveUsuario;
            ClavePerfil = clavePerfil;
        }
    }
}
