using HARD.CORE.DAT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG
{
    public class PrecioB
    {
        #region " Singleton "

        private static PrecioB instance = null;

        private static object mutex = new object();

        private PrecioB()
        {
        }

        public static PrecioB GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new PrecioB();
                }
            }

            return instance;

        }

        #endregion
        public DataTable ObtenerPorClienteProducto(int claveCliente, int claveProducto)
        {            
            return PreciosDA.GetInstance().ObtenerTodos(claveCliente, claveProducto);
        }
        public DataTable ObtenerDatosContrato(int claveCliente, int claveProducto)
        {            
            return PreciosDA.GetInstance().ObtenerDatosContrato(claveCliente, claveProducto);
        }

    }
}
