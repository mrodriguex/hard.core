namespace HARD.CORE.OBJ
{
    public class Estatus
    {

        public int ClaveEstatus { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public Estatus(int claveEstatus, string abreviatura, string descripcion, bool activo)
        {
            ClaveEstatus = claveEstatus;
            Abreviatura = abreviatura;
            Descripcion = descripcion;
            Activo = activo;
        }

    }
}
