namespace HARD.CORE.OBJ
{
    public class Menu
    {
        private int? _claveMenuPadre;

        public int ClaveMenu { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Ruta { get; set; }
        public int? ClaveMenuPadre { get { return (_claveMenuPadre); } set { _claveMenuPadre = value == 0 ? null : value; } }
        public int Orden { get; set; }
        public bool PerteneceAPerfil { get; set; }

        public Menu() { }

        public Menu(int claveMenu, string nombre, string imagen, string ruta, int? claveMenuPadre = null, int orden = 0, bool perteneceAPerfil = false)
        {
            ClaveMenu = claveMenu;
            Nombre = nombre;
            Ruta = ruta;
            Imagen = imagen;
            ClaveMenuPadre = (claveMenuPadre == 0 ? null : claveMenuPadre);
            Orden = orden;
            PerteneceAPerfil = perteneceAPerfil;
        }

    }
}
