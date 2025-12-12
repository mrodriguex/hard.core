namespace HARD.CORE.OBJ
{
    public class Pais
    {

        public int ClavePais { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public Pais(int clavePais, string descripcion, bool estatus)
        {
            ClavePais = clavePais;
            Descripcion = descripcion;
            Estatus = estatus;
        }

    }
}
