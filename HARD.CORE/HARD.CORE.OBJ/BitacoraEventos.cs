using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class BitacoraEventos
    {
        private string _descripcion;

        public int ClaveBitacoraEvento { get; set; }
        public int ClaveEntidad { get; set; }
        public int ClaveTipoEvento { get; set; }
        public int ClaveEvento { get; set; }
        public string ClaveUsuario { get; set; }
        public string Descripcion
        {
            get
            {
                if (string.IsNullOrEmpty(_descripcion))
                {
                    _descripcion = "Descripción no proporcionada.";
                }
                return _descripcion;
            }
            set { _descripcion = value; }
        }
        public DateTime? Fecha { get; set; }
        public BitacoraEventos() { }
    }
}
