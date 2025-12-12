namespace HARD.CORE.OBJ
{
    public class Colonia
    {

        public int Clave { get; set; }
        public int ClaveEstado { get; set; }
        public int ClaveMunicipio { get; set; }
        public string Descripcion { get; set; }
        public string TipoAsentamiento { get; set; }
        public string CodigoPostal { get; set; }

        public Colonia(int clave, int claveEstado, int claveMunicipio, string descripcion, string tipoAsentamiento, string codigoPostal)
        {
            Clave = clave;
            ClaveEstado = claveEstado;
            ClaveMunicipio = claveMunicipio;
            Descripcion = descripcion;
            TipoAsentamiento = tipoAsentamiento;
            CodigoPostal = codigoPostal;
        }

    }
}
