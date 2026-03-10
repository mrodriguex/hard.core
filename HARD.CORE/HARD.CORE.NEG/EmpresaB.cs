using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    public class EmpresaB : IEmpresaB
    {

        private readonly IRepositoryBase<Empresa, BaseFilter, int> _EmpresaDA;

        private readonly IRepositoryBase<Usuario, BaseFilter, int> _usuarioDA;

        public EmpresaB(IRepositoryBase<Empresa, BaseFilter, int> EmpresaDA,
            IRepositoryBase<Usuario, BaseFilter, int> usuarioDA)
        {
            _EmpresaDA = EmpresaDA;
            _usuarioDA = usuarioDA;
        }

        public IEnumerable<Empresa> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            return _EmpresaDA.GetAll(pagedFilter);
        }

        public Empresa GetById(int id)
        {
            return _EmpresaDA.GetById(id);
        }

        public bool Update(Empresa entity)
        {
            Empresa EmpresaModificacion = _EmpresaDA.GetById(entity.IdEmpresa);
            EmpresaModificacion.Nombre = entity.Nombre;
            EmpresaModificacion.Abreviatura = entity.Abreviatura;
            EmpresaModificacion.Activo = entity.Activo;
            EmpresaModificacion.Descripcion = entity.Descripcion;
            EmpresaModificacion.IdUsuarioModificacion = entity.IdUsuarioModificacion;
            EmpresaModificacion.Orden = entity.Orden;
            EmpresaModificacion.RazonSocial = entity.RazonSocial;
            EmpresaModificacion.RFC = entity.RFC;
            return _EmpresaDA.Update(EmpresaModificacion);
        }

        public bool Delete(int id)
        {
            return _EmpresaDA.Delete(id);
        }

        public int Add(Empresa entity)
        {
            return _EmpresaDA.Add(entity);
        }

        List<Empresa> IEmpresaB.GetCompaniesByUser(int? idUsuario)
        {
            Usuario usuario = _usuarioDA.GetById(idUsuario.Value);
            List<Empresa> empresas = new List<Empresa>();
            if (usuario != null)
            {
                empresas = usuario.Empresas ?? new List<Empresa>();
            }
            return empresas;
        }
    }
}
