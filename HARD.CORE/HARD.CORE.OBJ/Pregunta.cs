namespace HARD.CORE.OBJ
{
    public class Pregunta
    {

        public int ClavePregunta { get; set; }
        public string Descripcion { get; set; }
        public int Calificacion { get; set; }

        public Pregunta()
        {
        }

        public Pregunta(int clavePregunta, string descripcion, int calificacion)
        {
            ClavePregunta = clavePregunta;
            Descripcion = descripcion;
            Calificacion = calificacion;
        }

    }

}
