using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    public class ClienteB : IClienteB
    {

        private readonly IRepositoryBase<Cliente, BaseFilter, int> _clienteDA;

        public ClienteB(IRepositoryBase<Cliente, BaseFilter, int> clienteDA)
        {
            _clienteDA = clienteDA;
        }

        public IEnumerable<Cliente> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            return _clienteDA.GetAll(pagedFilter);
        }

        public Cliente GetById(int id)
        {
            return _clienteDA.GetById(id);
        }

        public bool Update(Cliente entity)
        {
            Cliente clienteModificacion = _clienteDA.GetById(entity.IdCliente);
            clienteModificacion.Nombre = entity.Nombre;
            clienteModificacion.Abreviatura = entity.Abreviatura;
            clienteModificacion.Activo = entity.Activo;
            clienteModificacion.Descripcion = entity.Descripcion;
            clienteModificacion.IdClientePadre = entity.IdClientePadre;
            clienteModificacion.IdUsuarioModificacion = entity.IdUsuarioModificacion;
            clienteModificacion.Orden = entity.Orden;
            clienteModificacion.RazonSocial = entity.RazonSocial;
            clienteModificacion.RFC = entity.RFC;
            return _clienteDA.Update(clienteModificacion);
        }

        public bool Delete(int id)
        {
            return _clienteDA.Delete(id);
        }

        public int Add(Cliente entity)
        {
            return _clienteDA.Add(entity);
        }

    }
}
