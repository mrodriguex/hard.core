using System.Collections.Generic;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    /// <summary>
    /// Interface for the business logic layer of companies.
    /// </summary>
    public interface IEmpresaB: IRepositoryBase<Empresa, BaseFilter, int> 
    {
        /// <summary>
        /// Obtains a company by its unique key.
        /// </summary>
        /// <param name="claveEmpresa">The unique key identifying the company.</param>
        /// <returns>The company associated with the provided key.</returns>
        List<Empresa> GetCompaniesByUser(int? idUsuario = null);        
    }
}
