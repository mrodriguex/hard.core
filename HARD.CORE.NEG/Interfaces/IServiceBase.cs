using System.Collections.Generic;
using System.Threading.Tasks;
using HARD.CORE.OBJ.Models;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IServiceBase<MyClassIn, MyClassOut, FilterClass, IdType>
    {
        Task<WebResultModel<MyClassOut>> GetByIdAsync(IdType id);
        Task<WebResultModel<IEnumerable<MyClassOut>>> GetAllAsync(PagedFilter<FilterClass> pagedFilter);
        Task<WebResultModel<IdType>> AddAsync(MyClassIn entity, int idUsuarioAuenticado);
        Task<WebResultModel<bool>> UpdateAsync(MyClassIn entity, int idUsuarioAuenticado);
        Task<WebResultModel<bool>> DeleteAsync(IdType id, int idUsuarioAuenticado);
    }
}