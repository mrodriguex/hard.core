
using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class BitacoraAcessos
    {
        public string ClaveUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int NumeroIngresos { get; set; }
        public DateTime UltimaConexion { get; set; }
        public BitacoraAcessos() { }
        public BitacoraAcessos(string claveUsuario, string nombreUsuario, int numeroIngresos, DateTime ultimaConexion)
        {
            ClaveUsuario = claveUsuario;
            NombreUsuario = nombreUsuario;
            NumeroIngresos = numeroIngresos;
            UltimaConexion = ultimaConexion;
        }
    }
}
