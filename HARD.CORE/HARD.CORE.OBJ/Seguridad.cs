using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class Seguridad
    {
        public TipoEntidad TipoEntidad { get; set; }
        public bool Crear { get; set; }
        public bool Eliminar { get; set; }
        public bool Editar { get; set; }
        public bool Consultar { get; set; }
        public bool Exportar { get; set; }

        public Seguridad(TipoEntidad tipoEntidad, bool crear, bool eliminar, bool editar, bool consultar, bool exportar)
        {
            TipoEntidad = tipoEntidad;
            Crear = crear;
            Eliminar = eliminar;
            Editar = editar;
            Consultar = consultar;
            Exportar = exportar;
        }
    } 
}
