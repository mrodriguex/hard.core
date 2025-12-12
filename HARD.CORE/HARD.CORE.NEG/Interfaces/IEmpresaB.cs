using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG.Interfaces
{
    /// <summary>
    /// Interface for the business logic layer of companies.
    /// </summary>
    public interface IEmpresaB
    {

        /// <summary>
        /// Obtains all companies.
        /// </summary>
        /// <param name="clavePerfil">
        /// The unique key identifying the profile.
        /// </param>
        /// <param name="claveUsuario">
        /// The unique key identifying the user.
        /// </param>
        /// <returns>
        /// A list of companies matching the specified criteria.
        /// </returns>
        List<Empresa> ObtenerTodos(int? clavePerfil = null, string? claveUsuario = null);

        /// <summary>
        /// Obtains a company by its unique key.
        /// </summary>
        /// <param name="claveEmpresa">The unique key identifying the company.</param>
        /// <returns>The company associated with the provided key.</returns>
        List<Empresa> ObtenerEmpresasUsuario(string claveUsuario = null);
        Empresa Obtener(int? claveEmpresa = null);
        bool InsertarEmpresaUsuario(Empresa empresa, string claveUsuario);
        bool EliminarEmpresasUsuario(string claveUsuario);
    }
}
