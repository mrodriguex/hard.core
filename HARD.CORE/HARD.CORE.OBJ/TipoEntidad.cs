using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class TipoEntidad
    {
        public int ClaveTipoEntidad { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public string ClaveUsuarioUltimaModificacion { get; set; }

        public TipoEntidad(int claveTipoEntidad, string abreviatura, string descripcion, bool estatus, string claveUsuarioUltimaModificacion)
        {
            ClaveTipoEntidad = claveTipoEntidad;
            Abreviatura = abreviatura;
            Descripcion = descripcion;
            Estatus = estatus;
            ClaveUsuarioUltimaModificacion = claveUsuarioUltimaModificacion;
        }
    }
}
