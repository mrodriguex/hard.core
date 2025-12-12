using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG
{
    public class FlujoAutorizacionB : IFlujoAutorizacionB
    {
        IFlujoAutorizacionDA _flujoAutorizacionDA;
        public FlujoAutorizacionB(IFlujoAutorizacionDA flujoAutorizacionDA)
        {
            _flujoAutorizacionDA = flujoAutorizacionDA;
        }
        public List<FlujoAutorizacion> ObtenerTodos()
        {
            return _flujoAutorizacionDA.ObtenerTodos();
        }
    }
}
