// HARD.CORE.NEG/Interfaces/ICorreoB.cs
using System.Collections.Generic;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    public interface ICorreoB
    {
        bool Actualizar(Correo correo);
        bool EnviarCorreo(int ClaveCorreo, int ClaveRequisicion, string ClaveUsuario);
        int Insertar(Correo correo);
        Correo Obtener(int claveCorreo);
        List<Correo> ObtenerTodos(bool? estatus = false);
    }
}