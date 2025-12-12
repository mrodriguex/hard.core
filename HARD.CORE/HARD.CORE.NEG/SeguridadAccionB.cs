using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG
{
    public class SeguridadAccionB : ISeguridadAccionB
    {
        ISeguridadAccionDA _seguridadAccionDA;
        public SeguridadAccionB(ISeguridadAccionDA seguridadAccionDA)
        {
            _seguridadAccionDA = seguridadAccionDA;
        }
        public List<SeguridadAccion> ObtenerTodos(int clavePerfil)
        {
            return _seguridadAccionDA.ObtenerTodos(clavePerfil);
        }
        public SeguridadAccion Obtener(int clavePerfil, int claveMenu)
        {
            return _seguridadAccionDA.Obtener(clavePerfil, claveMenu);
        }
        public bool Actualizar(List<SeguridadAccion> seguridadAccion)
        {
            return _seguridadAccionDA.Actualizar(seguridadAccion);
        }
    }
}