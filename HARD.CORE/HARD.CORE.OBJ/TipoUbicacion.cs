namespace HARD.CORE.OBJ
{
    public class TipoUbicacion
    {

        public int ClaveTipoUbicacion { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public TipoUbicacion(int claveTipoUbicacion, string abreviatura, string descripcion, bool estatus)
        {
            ClaveTipoUbicacion = claveTipoUbicacion;
            Abreviatura = abreviatura;
            Descripcion = descripcion;
            Estatus = estatus;
        }

    }

}
