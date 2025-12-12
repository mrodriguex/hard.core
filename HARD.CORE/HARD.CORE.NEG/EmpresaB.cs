using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System.Collections.Generic;
using System.Linq;

namespace HARD.CORE.NEG
{
    /// <summary>
    /// Business logic layer for managing companies.
    /// </summary>
    public class EmpresaB : IEmpresaB
    {
        private IEmpresaDA _empresaDA;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmpresaB"/> class.
        /// </summary>
        /// <param name="empresaDA">The company data access layer.</param>
        /// <param name="empresaDA"></param>
        public EmpresaB(IEmpresaDA empresaDA)
        {
            _empresaDA = empresaDA;
        }

        /// <summary>
        /// Obtains a company by its unique key.
        /// </summary>
        /// <param name="claveEmpresa">The unique key identifying the company.</param>
        /// <returns>The company associated with the provided key.</returns>
        public Empresa Obtener(int? claveEmpresa)
        {
            return _empresaDA.Obtener(claveEmpresa: claveEmpresa);
        }

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
        public List<Empresa> ObtenerTodos(int? clavePerfil = null, string? claveUsuario = null)
        {
            //if (clavePerfil.HasValue && clavePerfil.Value == 1)
            if (string.IsNullOrEmpty(claveUsuario))
            {
                return _empresaDA.ObtenerTodos();
            }
            else
            {
                return _empresaDA.ObtenerEmpresasUsuario(claveUsuario: claveUsuario);
            }
        }

        public bool EliminarEmpresasUsuario(string claveUsuario)
        {
            return _empresaDA.EliminarEmpresasUsuario(claveUsuario);
        }

        public bool InsertarEmpresaUsuario(Empresa empresa, string claveUsuario)
        {
            return _empresaDA.InsertarEmpresaUsuario(empresa, claveUsuario);
        }

        public List<Empresa> ObtenerEmpresasUsuario(string claveUsuario)
        {
            return _empresaDA.ObtenerEmpresasUsuario(claveUsuario);
        }
    }
}
