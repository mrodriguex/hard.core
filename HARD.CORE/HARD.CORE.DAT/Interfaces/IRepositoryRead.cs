using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HARD.CORE.DAT.Interfaces
{
    public interface IRepositoryRead<MyClass, FilterClass, IdType>
    {
        MyClass GetById(IdType id);
        IEnumerable<MyClass> GetAll(PagedFilter<FilterClass> pagedFilter);
    }
}
