using HARD.CORE.OBJ;

using System.Data;

namespace HARD.CORE.DAT.Interfaces
{
    public interface ISugerenciaDA
    {
        public int InsertarSugerencia(Sugerencia sugerencia, bool esAnonimo);

        public DataTable ObtenerSugerencias();
    }
}
