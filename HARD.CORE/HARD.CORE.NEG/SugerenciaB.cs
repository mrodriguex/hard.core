using HARD.CORE.NEG.Interfaces;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;

using System;
using System.Data;

namespace HARD.CORE.NEG
{
    public class SugerenciaB: ISugerenciaB
    {
        private readonly ISugerenciaDA _sugerenciaDA;
        private readonly IUsuarioB _usuarioB;
        private readonly ICorreoB _correoB;

        public SugerenciaB(IUsuarioB usuarioB, ICorreoB correoB, ISugerenciaDA sugerenciaDA)
        {
            _usuarioB = usuarioB;
            _correoB = correoB;
            _sugerenciaDA = sugerenciaDA;
        }

        public DataTable ObtenerSugerencias()
        {
            return _sugerenciaDA.ObtenerSugerencias();
        }

    }
}
