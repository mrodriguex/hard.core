using System;
using System.Collections.Generic;

namespace HARD.CORE.OBJ
{
    public class Perfil
    {

        public int ClavePerfil { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public string ClaveUsuarioAlta { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string ClaveUsuarioUltimaActualizacion { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        private List<Menu> _menus;

        public List<Menu> Menus { get { if (_menus == null) { _menus = new List<Menu>(); } return _menus; } set { _menus = value; } }


        public Perfil()
        {
        }

        public Perfil(int clavePerfil, string descripcion, bool estatus, string claveUsuarioAlta = null, DateTime? fechaAlta = null,
            string claveUsuarioUltimaActualizacion = null, DateTime? fechaUltimaActualizacion = null)
        {
            ClavePerfil = clavePerfil;
            Descripcion = descripcion;
            Estatus = estatus;
            ClaveUsuarioAlta = claveUsuarioAlta;
            FechaAlta = fechaAlta;
            ClaveUsuarioUltimaActualizacion = claveUsuarioUltimaActualizacion;
            FechaUltimaActualizacion = fechaUltimaActualizacion;
        }

    }

}
