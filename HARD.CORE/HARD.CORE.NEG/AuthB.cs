using System.Collections.Generic;
using System.Linq;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

namespace HARD.CORE.NEG
{
    public class AuthB
    {
        private readonly IUsuarioB _usuarioB;

        public AuthB(IUsuarioB usuarioB)
        {
            _usuarioB = usuarioB;
        }

        public bool ValidateUser(string username, string password)
        {
            bool success = false;

            BaseFilter baseFilter = new BaseFilter() { Nombre = username };
            PagedFilter<BaseFilter> filter = new PagedFilter<BaseFilter> { PageIndex = 1, PageSize = int.MaxValue, Filters = baseFilter };

            List<Usuario> usuarios = _usuarioB.GetAll(filter).ToList();
            Usuario usuario = usuarios.FirstOrDefault();

            if (!string.IsNullOrEmpty(usuario.ClaveUsuario) && username.ToLower() == usuario.ClaveUsuario.ToLower())
            {
                // Validar credenciales contra la base de datos
                success = _usuarioB.AuthenticateUser(usuario.Id, password);
            }
            return (success);
        }

    }
}
