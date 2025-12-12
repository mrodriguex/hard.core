using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG
{
    public class MotivoVacanteB : IMotivoVacanteB
    {
        IMotivoVacanteDA _motivoVacanteDA;

        /// <summary>
        /// Constructor de la clase MenuB
        /// </summary>
        /// <param name="menuDA">
        /// Interfaz para el acceso a datos del menú
        /// </param>
        /// <param name="usuarioB">
        /// Interfaz para el acceso a datos del usuario
        /// </param>
        public MotivoVacanteB(IMotivoVacanteDA motivoVacanteDA)
        {
            _motivoVacanteDA = motivoVacanteDA;
        }
        public List<MotivoVacante> ObtenerTodos()
        {
            return _motivoVacanteDA.ObtenerTodos();
        }
        public bool Actualizar(MotivoVacante motivoVacante)
        {
            return _motivoVacanteDA.Actualizar(motivoVacante);
        }

    }
}
