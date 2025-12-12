namespace HARD.CORE.OBJ
{
    public class TipoContenido
    {
        public int ClaveTipoContenido { get; set; }
        public string Descripcion { get; set; }
        public TipoContenido(int claveTipoContenido, string descripcion)
        {
            ClaveTipoContenido = claveTipoContenido;
            Descripcion = descripcion;
        }
    }
}
