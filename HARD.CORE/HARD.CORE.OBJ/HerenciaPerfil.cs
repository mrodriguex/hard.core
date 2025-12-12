using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class HerenciaPerfil
    {
        public int ClaveHerenciaPerfil { get; set; }
        public string ClaveUsuario { get; set; }
        public string ClaveUsuarioHeredado { get; set; }
        public string UsuarioHeredado { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public bool Estatus { get; set; }
    }
}