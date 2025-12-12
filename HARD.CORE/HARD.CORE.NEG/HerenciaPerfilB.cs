using HARD.CORE.OBJ;

using System;
using System.Collections.Generic;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.DAT.Interfaces;

namespace HARD.CORE.DAT
{
    public class HerenciaPerfilB : IHerenciaPerfilB
    {

        private readonly IHerenciaPerfilDA _herenciaPerfilDA;

        public HerenciaPerfilB(IHerenciaPerfilDA herenciaPerfilDA)
        {
            _herenciaPerfilDA = herenciaPerfilDA;
        }
     
        public bool Actualizar(HerenciaPerfil perfil)
        {
            return _herenciaPerfilDA.Actualizar(perfil);
        }

        public bool Existe(HerenciaPerfil perfil)
        {
            return _herenciaPerfilDA.Existe(perfil);
        }

        public int Insertar(HerenciaPerfil perfil)
        {
            return _herenciaPerfilDA.Insertar(perfil);
        }

        public List<HerenciaPerfil> ObtenerTodos(bool? estatus)
        {
            return _herenciaPerfilDA.ObtenerTodos(estatus);
        }
       
    }

}