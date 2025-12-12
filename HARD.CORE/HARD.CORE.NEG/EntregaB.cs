using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HARD.CORE.DAT;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG
{
    public class EntregaB
    {
        #region "Singleton"

        private static EntregaB instance = null;

        private static object mutex = new object();
        private EntregaB()
        {
        }

        public static EntregaB GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new EntregaB();
                }
            }

            return instance;

        }

        #endregion
        public DataTable ObtenerClavesArrendamientos(int? claveCliente = null)
        {
            return EntregasDA.GetInstance().ObtenerClavesArrendamientos(claveCliente);
        }
        public DataTable ObtenerPorCliente(int claveCliente, DateTime fechaInicio, DateTime fechaFin, int? claveProducto = null)
        {
            if (claveProducto != 0)
                return EntregasDA.GetInstance().ObtenerEntregas(claveCliente, claveProducto, fechaInicio, fechaFin);
            else
                return EntregasDA.GetInstance().ObtenerEntregas(claveCliente, null, fechaInicio, fechaFin);
        }        
        public DataTable ObtenerArrendamientosPorCliente(int claveCliente, DateTime fechaInicio, DateTime fechaFin, string claveArrendamiento = "")
        {
            if (claveArrendamiento != string.Empty)
                return EntregasDA.GetInstance().ObtenerArrendamientos(claveCliente, claveArrendamiento, fechaInicio, fechaFin);
            else
                return EntregasDA.GetInstance().ObtenerArrendamientos(claveCliente, null, fechaInicio, fechaFin);
        }

    }
}
