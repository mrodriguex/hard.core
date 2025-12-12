using System;

namespace HARD.CORE.OBJ
{
    public class Sugerencia
    {

        public int ClaveSugerencia { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Comentarios { get; set; }
        public string ClaveUsuario { get; set; }
        DateTime Fecha { get; set; }

        public Sugerencia() { }

        public Sugerencia(int claveSugerencia, string nombre, string correo, string telefono, string comentarios, DateTime fecha)
        {
            ClaveSugerencia = claveSugerencia;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            Comentarios = comentarios;
            Fecha = fecha;
        }

    }
}
