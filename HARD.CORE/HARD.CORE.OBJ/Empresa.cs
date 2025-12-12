using System;
using System.Collections.Generic;
using System.Text;

namespace HARD.CORE.OBJ
{
    public class Empresa
    {
        public int ClaveEmpresa { get; set; }
        public string Descripcion { get; set; }
        public Empresa() { }
        public Empresa(int claveEmpresa, string descripcion)
        {
            ClaveEmpresa = claveEmpresa;
            Descripcion = descripcion;
        }
    }
}
