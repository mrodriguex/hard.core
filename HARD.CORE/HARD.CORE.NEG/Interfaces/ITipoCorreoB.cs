using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.NEG.Interfaces
{
    public interface ITipoCorreoB
    {
        TipoCorreo Obtener(int claveTipoCorreo);
        List<TipoCorreo> ObtenerTodos(bool? estatus = false);
    }
}




