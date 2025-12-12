using HARD.CORE.DAT;
using HARD.CORE.OBJ;

using System.Data;

namespace HARD.CORE.NEG
{
    public class ClienteB
    {

        #region "Singleton"

        private static ClienteB instance = null;

        private static object mutex = new object();
        private ClienteB()
        {
        }

        public static ClienteB GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new ClienteB();
                }
            }

            return instance;

        }

        #endregion

        public DataTable ObtenerTodos()
        {
            return ClienteDA.GetInstance().ObtenerTodos();
        }

        public Cliente Obtener(int claveCliente)
        {
            return ClienteDA.GetInstance().ObtenerCliente(claveCliente);
        }
        public DataTable ObtenerActivos()           
        {
            return ClienteDA.GetInstance().ObtenerClientes();
        }
        public DataTable ObtenerNoRegistrados()
        {
            return ClienteDA.GetInstance().ObtenerNoRegistrados();
        }              

    }
}
