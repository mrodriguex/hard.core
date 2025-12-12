using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    public class AvisoB : IAvisoB
    {

        IAvisoDA _avisoDA;

        public AvisoB(IAvisoDA avisoDA)
        {
            _avisoDA = avisoDA;
        }

        public Aviso Obtener(int claveAviso)
        {
            return _avisoDA.Obtener(claveAviso);
        }
        public List<Aviso> ObtenerTodos(bool? estatus = null)
        {
            List<Aviso> avisos = _avisoDA.ObtenerTodos(estatus: estatus);
            return avisos;
        }
        public List<Aviso> ObtenerActivosLista()
        {
            List<Aviso> avisos = _avisoDA.ObtenerActivosLista();
            return avisos;
        }

        public bool PuedeInsertarAviso()
        {
            List<Aviso> avisosActivos = _avisoDA.ObtenerTodos(estatus: true);
            return avisosActivos.Count < (int)EnumConstante.TotalAvisos + 1;
        }
        public int Insertar(Aviso aviso)
        {
            return _avisoDA.Insertar(aviso);
        }
        public bool Actualizar(Aviso aviso)
        {
            return _avisoDA.Actualizar(aviso);
        }
        public bool Eliminar(int ClaveAviso, string ClaveUsuario)
        {
            Aviso aviso = Obtener(ClaveAviso);
            aviso.ClaveUsuarioActualizacion = ClaveUsuario;
            return _avisoDA.Eliminar(aviso);
        }

    }

}
