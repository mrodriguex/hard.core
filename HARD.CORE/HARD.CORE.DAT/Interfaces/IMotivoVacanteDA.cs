using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IMotivoVacanteDA
    {
        List<MotivoVacante> ObtenerTodos(bool? estatus = null);
        bool Actualizar(MotivoVacante motivoVacante);
    }
}
