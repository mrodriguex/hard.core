using System.Collections.Generic;
using System.Threading.Tasks;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IClienteService : IServiceBase<Cliente, Cliente, BaseFilter, int>
    {
        public Task<WebResultModel<IEnumerable<Cliente>>> GetAllAsync(bool? activo = null, int? idUsuario = null, int? idPerfil = null, int? pageIndex = null, int? pageSize = null);
    }
}