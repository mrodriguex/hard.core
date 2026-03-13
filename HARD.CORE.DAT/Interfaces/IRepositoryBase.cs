using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HARD.CORE.DAT.Interfaces
{
    public interface IRepositoryBase<MyClass, FilterClass, IdType>
    {
        MyClass GetById(IdType id);
        IEnumerable<MyClass> GetAll(PagedFilter<FilterClass> pagedFilter);
        IdType Add(MyClass entity);
        bool Update(MyClass entity);
        bool Delete(IdType id);
    }
}
