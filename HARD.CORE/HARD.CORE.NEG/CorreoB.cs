using HARD.CORE.OBJ;

using HARD.CORE.NEG.Interfaces;
using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    public class CorreoB : ICorreoB
    {
        private readonly ICorreoDA _correoDA;

        public CorreoB(ICorreoDA correoDA)
        {
            _correoDA = correoDA;
        }

        public bool Actualizar(Correo correo)
        {
            return _correoDA.Actualizar(correo);
        }

        public int Insertar(Correo correo)
        {
            return _correoDA.Insertar(correo);
        }

        public Correo Obtener(int claveCorreo)
        {
            return _correoDA.Obtener(claveCorreo);
        }

        public List<Correo> ObtenerTodos(bool? estatus = false)
        {
            return _correoDA.ObtenerTodos(estatus: estatus);
        }

        public bool EnviarCorreo(int claveCorreo, int claveRequisicion, string claveUsuario)
        {
            return _correoDA.EnviarCorreo(claveCorreo: claveCorreo, claveRequisicion: claveRequisicion, claveUsuario: claveUsuario);
        }
    }
}
