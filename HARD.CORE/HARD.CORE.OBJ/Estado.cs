namespace HARD.CORE.OBJ
{
    public class Estado
    {

        public int ClaveEstado { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public Estado()
        {
        }

        public Estado(int claveEstado, string abreviatura, string descripcion, bool estatus)
        {
            ClaveEstado = claveEstado;
            Abreviatura = abreviatura;
            Descripcion = descripcion;
            Estatus = estatus;
        }

    }
}
