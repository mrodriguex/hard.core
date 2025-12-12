using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IMotivoVacanteB
    {
        List<MotivoVacante> ObtenerTodos();
        bool Actualizar(MotivoVacante motivoVacante);
    }
}
