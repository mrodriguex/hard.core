using System.Collections.Generic;

namespace HARD.CORE.OBJ
{
    public class Encuesta
    {

        public int ClaveEncuesta { get; set; }
        public string NombreEncuesta { get; set; }
        public bool Estatus { get; set; }
        public List<Pregunta> Preguntas { get; set; }

        public Encuesta()
        {

        }

        public Encuesta(int claveEncuesta, string nombreEncuesta, bool estatus, List<Pregunta> preguntas)
        {
            ClaveEncuesta = claveEncuesta;
            NombreEncuesta = nombreEncuesta;
            Estatus = estatus;
            Preguntas = preguntas;
        }

    }

}
