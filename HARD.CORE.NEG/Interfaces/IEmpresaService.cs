using System.Collections.Generic;
using System.Threading.Tasks;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.OBJ.Models;

namespace HARD.CORE.NEG.Interfaces
{
    /// <summary>
    /// Interface for the business logic layer of companies.
    /// </summary>
    public interface IEmpresaService : IServiceBase<Empresa, Empresa, BaseFilter, int>
    {

        public Task<WebResultModel<IEnumerable<Empresa>>> GetAllAsync(bool? activo = null, int? idUsuario = null, int? idPerfil = null, int? pageIndex = null, int? pageSize = null);

        /// <summary>
        /// Obtains a company by its unique key.
        /// </summary>
        /// <param name="claveEmpresa">The unique key identifying the company.</param>
        /// <returns>The company associated with the provided key.</returns>
        public Task<WebResultModel<IEnumerable<Empresa>>> GetCompaniesByUserAsync(int idUsuario, int? pageIndex = null, int? pageSize = null);

    }
}
