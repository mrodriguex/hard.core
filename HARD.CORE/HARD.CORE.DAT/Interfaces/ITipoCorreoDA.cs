using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.DAT.Interfaces
{
    public interface ITipoCorreoDA
    {
        TipoCorreo Obtener(int claveTipoCorreo);
        List<TipoCorreo> ObtenerTodos(bool? estatus = false);
    }
}




