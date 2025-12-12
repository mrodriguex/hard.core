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
            Usuario usuario = _usuarioB.Obtener(claveUsuario: username);

            if (!string.IsNullOrEmpty(usuario.ClaveUsuario) && username.ToLower() == usuario.ClaveUsuario.ToLower())
            {
                // Validar credenciales contra la base de datos
                success = _usuarioB.AutenticarUsuario(claveUsuario: usuario.ClaveUsuario, password: password);
            }
            return (success);
        }

    }
}
