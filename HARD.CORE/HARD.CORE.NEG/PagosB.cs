using HARD.CORE.DAT;

using System.Data;

namespace HARD.CORE.NEG
{
    public class PagosB
    {

        #region " Singleton "

        private static PagosB instance = null;

        private static object mutex = new object();
        private PagosB()
        {
        }

        public static PagosB GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PagosB();
                }
            }

            return instance;

        }

        #endregion

        public DataTable ObtenerReporteCartera(int claveCliente)
        {
            return PagosDA.GetInstance().ObtenerReporteCartera(claveCliente);
        }

    }
}
