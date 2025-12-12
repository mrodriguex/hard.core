using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class Rutas
    {
        public Rutas(int claveRuta, string descripcion, string ruta)
        {
            ClaveRuta = claveRuta;
            Descripcion = descripcion;
            Ruta = ruta;

        }
        public int ClaveRuta { get; set; }
        public string Descripcion { get; set; }
        public string Ruta { get; set; }

    }
}
