using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.DAT.Interfaces
{
    public interface ICorreoVariableDA
    {
        CorreoVariable Obtener(int claveCorreoVariable);
        List<CorreoVariable> ObtenerTodos(int? claveTipoCorreo = null ,bool? estatus = false);
    }
}




