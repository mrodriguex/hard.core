namespace HARD.CORE.OBJ
{
    public class Municipio
    {

        public int ClaveEstado { get; set; }
        public int ClaveMunicipio { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public Municipio(int claveEstado, int claveMunicipio, string descripcion, bool estatus)
        {
            ClaveEstado = claveEstado;
            ClaveMunicipio = claveMunicipio;
            Descripcion = descripcion;
            Estatus = estatus;
        }

    }
}
