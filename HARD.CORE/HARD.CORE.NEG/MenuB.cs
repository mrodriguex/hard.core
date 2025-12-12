using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    /// <summary>
    /// Clase para la gestión de menús
    /// </summary>
    public class MenuB : IMenuB
    {

        IMenuDA _menuDA;
        IUsuarioB _usuarioB;

        /// <summary>
        /// Constructor de la clase MenuB
        /// </summary>
        /// <param name="menuDA">
        /// Interfaz para el acceso a datos del menú
        /// </param>
        /// <param name="usuarioB">
        /// Interfaz para el acceso a datos del usuario
        /// </param>
        public MenuB(IMenuDA menuDA, IUsuarioB usuarioB)
        {
            _menuDA = menuDA;
            _usuarioB = usuarioB;
        }

        /// <summary>
        /// Obtiene el menú para un usuario específico.
        /// </summary>
        /// <param name="claveUsuario">
        /// Clave del usuario
        /// </param>
        /// <param name="clavePerfil">
        /// Clave del perfil
        /// </param>
        /// <returns>
        /// Lista de menús para el usuario
        /// </returns>
        public List<Menu> ObtenerMenu_Usuario(string claveUsuario, int clavePerfil)
        {
            List<Menu> menusInicial = _menuDA.ObtenerMenu_Usuario(claveUsuario, clavePerfil);
            List<Menu> menus = new List<Menu>();
            Usuario usuario = _usuarioB.Obtener(claveUsuario);

            /// Si el usuario está activo, se filtran los menús
            if (usuario.esActive)
            {
                /// Se excluyen los menús que comienzan con "cambio de contra" para los usuarios de red (ActiveDirectory)                
                menus = menusInicial.FindAll(m => !m.Nombre.ToLower().StartsWith("cambio de contra"));
            }
            else
            {
                /// Si el usuario no es de ActiveDirectory, se muestran todos los menús
                menus = menusInicial;
            }

            return menus;
        }

        /// <summary>
        /// Obtiene el menú para un perfil específico.
        /// </summary>
        /// <param name="clavePerfil">
        /// Clave del perfil
        /// </param>
        /// <returns>
        /// Lista de menús para el perfil
        /// </returns>
        public List<Menu> ObtenerMenu_Perfil(int clavePerfil)
        {
            if (clavePerfil > 0)
            {
                return _menuDA.ObtenerMenu_Perfil(clavePerfil);
            }
            else
            {
                return _menuDA.ObtenerTodos(estatus: true);
            }
        }

        /// <summary>
        /// Configura el menú para un perfil específico.
        /// </summary>
        /// <param name="clavePerfil">
        /// Clave del perfil
        /// </param>
        /// <param name="menus">
        /// Lista de menús a configurar
        /// </param>
        /// <returns>
        /// Verdadero si la configuración fue exitosa, falso en caso contrario
        /// </returns>
        public bool ConfigurarMenu_Perfil(int clavePerfil, List<Menu> menus)
        {
            return _menuDA.ConfigurarMenu_Perfil(clavePerfil, menus);
        }

        /// <summary>
        /// Obtiene un menú específico.
        /// </summary>
        /// <param name="claveMenu">
        /// Clave del menú
        /// </param>
        /// <returns>
        /// Menú específico
        /// </returns>
        public Menu Obtener(int claveMenu)
        {
            return _menuDA.Obtener(claveMenu);
        }

        /// <summary>
        /// Obtiene todos los menús.
        /// </summary>
        /// <param name="claveEstatus">
        /// Clave del estatus
        /// </param>
        /// <returns>
        /// Lista de menús
        /// </returns>
        public List<Menu> ObtenerTodos(bool? claveEstatus = null)
        {
            return _menuDA.ObtenerTodos(claveEstatus);
        }

        /// <summary>
        /// Inserta un nuevo menú.
        /// </summary>
        /// <param name="Menu">
        /// Menú a insertar
        /// </param>
        /// <returns>
        /// Clave del menú insertado
        /// </returns>
        public int Insertar(Menu Menu)
        {
            return _menuDA.Insertar(Menu);
        }

        /// <summary>
        /// Actualiza un menú existente.
        /// </summary>
        /// <param name="Menu">
        /// Menú a actualizar
        /// </param>
        /// <returns>
        /// Verdadero si la actualización fue exitosa, falso en caso contrario
        /// </returns>
        public bool Actualizar(Menu Menu)
        {
            return _menuDA.Actualizar(Menu);
        }

    }

}
