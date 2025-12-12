using System;

namespace HARD.CORE.OBJ
{
    public class Cliente
    {

        public int ClaveCliente { get; set; } 
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public int ClaveClientePadre { get; set; }

        public Cliente()
        {

        }
        public Cliente(int claveCliente, string rfc, string razonSocial, int claveClientePadre)
        {
            ClaveCliente = claveCliente;
            RFC = rfc;
            RazonSocial = razonSocial;
            ClaveClientePadre = claveClientePadre;
        }
    }
}
