using System;

namespace HARD.CORE.OBJ
{
    public class Cliente: Base
    {

        public int IdCliente => Id;
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public int? IdClientePadre { get; set; }

    }
}
