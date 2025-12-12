using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class SeguridadAccion
    {
        public int ClaveSeguridadAccion { get; set; }
        public int ClavePerfil { get; set; }
        public int ClaveMenu { get; set; }
        public string Descripcion { get; set; }
        public bool Crear { get; set; }
        public bool Modificar { get; set; }
        public bool Consultar { get; set; }
        public bool Eliminar { get; set; }
        public bool Autorizar { get; set; }
        public bool Rechazar { get; set; }
        public bool Imprimir { get; set; }
        public bool Asignar { get; set; }
        public bool Cancelar { get; set; }
        public int Orden { get; set; }
        public string ClaveUsuarioUltimaActualizacion { get; set; }
        public EnumAccion EnumAccion { get; set; }
        public SeguridadAccion() { }

    }
}