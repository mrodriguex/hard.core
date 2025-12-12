using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class Correo
    {
        public int ClaveCorreo { get; set; }
        public string Asunto { get; set; }
        public string Para { get; set; }
        public string CC { get; set; }
        public string CCO { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Cuerpo { get; set; }
        public string Pie { get; set; }
        public string Importancia { get; set; }
        public string Archivos { get; set; }
        public int ClaveTipoCorreo { get; set; }
        public string TipoCorreo { get; set; }
        public bool Estatus { get; set; }
        public string ClaveUsuarioActualizacion { get; set; }
        public Correo()
        {

        }

    }
}
