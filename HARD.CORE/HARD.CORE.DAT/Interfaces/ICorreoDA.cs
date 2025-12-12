using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Configuration;

using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.DAT.Interfaces
{
    public interface ICorreoDA
    {
        bool Actualizar(Correo correo);
        bool EnviarCorreo(int claveCorreo, int claveRequisicion, string claveUsuario);
        int Insertar(Correo correo);
        Correo Obtener(int claveCorreo);
        List<Correo> ObtenerTodos(bool? estatus = false);
    }
}




