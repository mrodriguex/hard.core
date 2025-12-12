namespace HARD.CORE.OBJ
{
    public class Contacto
    {

        public int ClaveContacto { get; set; }
        public int ClaveCliente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public string Puesto { get; set; }
        public string Correo { get; set; }
        public string Notas { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Fotografia { get; set; }
        public bool Estatus { get; set; }
        public string ClaveUsuarioActualizacion { get; set; }
        public byte[] Imagen { get; set; }

        public Contacto()
        {
        }

        public Contacto(int claveContacto, int claveCliente, string nombre, string apellidoPaterno, string apellidoMaterno, string puesto, string correo,
            string notas, string telefono, string celular, string fotografia, bool estatus, byte[] imagen)
        {
            ClaveContacto = claveContacto;
            ClaveCliente = claveCliente;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            NombreCompleto = string.Concat(nombre.Trim(), " ", apellidoPaterno.Trim(), " ", apellidoMaterno.Trim());
            Puesto = puesto;
            Correo = correo;
            Notas = notas;
            Telefono = telefono;
            Celular = celular;
            Fotografia = fotografia;
            Estatus = estatus;
            Imagen = imagen;
        }

    }
}
