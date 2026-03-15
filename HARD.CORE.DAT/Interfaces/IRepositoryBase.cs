using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HARD.CORE.DAT.Interfaces
{
    public interface IRepositoryBase<MyClass, FilterClass, IdType>
    {
        Task<MyClass> GetByIdAsync(IdType id);
        Task<IEnumerable<MyClass>> GetAllAsync(PagedFilter<FilterClass> pagedFilter);
        Task<IdType> AddAsync(MyClass entity);
        Task<bool> UpdateAsync(MyClass entity);
        Task<bool> DeleteAsync(IdType id);
    }
}
