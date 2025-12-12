using HARD.CORE.OBJ;

using System.Collections.Generic;

namespace HARD.CORE.NEG.Interfaces
{
    public interface ICorreoVariableB
    {
        CorreoVariable Obtener(int claveCorreoVariable);
        List<CorreoVariable> ObtenerTodos(int? claveTipoCorreo = null, bool? estatus = false);
    }
}




