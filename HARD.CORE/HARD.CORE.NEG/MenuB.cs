using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using System.Collections.Generic;
using System.Linq;

namespace HARD.CORE.NEG
{
    /// <summary>
    /// Clase para la gestión de menús
    /// </summary>
    public class MenuB : IMenuB
    {

        IRepositoryBase<Menu, BaseFilter, int> _menuDA;
        IRepositoryBase<Perfil, BaseFilter, int> _perfilDA;
        IRepositoryBase<Usuario, BaseFilter, int> _usuarioDA;

        /// <summary>
        /// Constructor de la clase MenuB
        /// </summary>
        /// <param name="menuDA">
        /// Interfaz para el acceso a datos del menú
        /// </param>
        /// <param name="perfilDA">
        /// Interfaz para el acceso a datos del perfil
        /// </param>
        /// <param name="usuarioDA">
        /// Interfaz para el acceso a datos del usuario
        /// </param>
        public MenuB(IRepositoryBase<Menu, BaseFilter, int> menuDA,
        IRepositoryBase<Perfil, BaseFilter, int> perfilDA,
        IRepositoryBase<Usuario, BaseFilter, int> usuarioDA)
        {
            _menuDA = menuDA;
            _perfilDA = perfilDA;
            _usuarioDA = usuarioDA;
        }

        /// <summary>
        /// Obtiene un menú específico.
        /// </summary>
        /// <param name="id">
        /// Clave del menú
        /// </param>
        /// <returns>
        /// Menú específico
        /// </returns>
        public Menu GetById(int id)
        {
            return _menuDA.GetById(id);
        }

        /// <summary>
        /// Obtiene todos los menús.
        /// </summary>
        /// <param name="pagedFilter">
        /// Filtro paginadodo para la consulta de menús
        /// </param>
        /// <returns>
        /// Lista de menús
        /// </returns>
        public IEnumerable<Menu> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            return _menuDA.GetAll(pagedFilter);
        }

        /// <summary>
        /// Inserta un nuevo menú.
        /// </summary>
        /// <param name="menu">
        /// Menú a insertar
        /// </param>
        /// <returns>
        /// Clave del menú insertado
        /// </returns>
        public int Add(Menu menu)
        {
            return _menuDA.Add(menu);
        }

        /// <summary>
        /// Actualiza un menú existente.
        /// </summary>
        /// <param name="menu">
        /// Menú a actualizar
        /// </param>
        /// <returns>
        /// Verdadero si la actualización fue exitosa, falso en caso contrario
        /// </returns>
        public bool Update(Menu menu)
        {
            return _menuDA.Update(menu);
        }

        /// <summary>
        /// Elimina un menú por su clave única.
        /// </summary>
        /// <param name="id">
        /// Clave del menú a eliminar
        /// </param>
        /// <returns>
        /// Verdadero si la eliminación fue exitosa, falso en caso contrario
        /// </returns>
        public bool Delete(int id)
        {
            return _menuDA.Delete(id);
        }

        /// <summary>
        /// Obtiene el menú para un usuario específico.
        /// </summary>
        /// <param name="idUsuario">
        /// Clave del usuario
        /// </param>
        /// <param name="idPerfil">
        /// Clave del perfil
        /// </param>
        /// <returns>
        /// Lista de menús para el usuario
        /// </returns>
        public List<Menu> GetMenusByUser(int idUsuario, int idPerfil)
        {
            Usuario usuario = _usuarioDA.GetById(idUsuario);
            List<Menu> menus = usuario.Perfiles.Where(p => p.IdPerfil == idPerfil)
                .SelectMany(p => p.Menus)
                .ToList();
            return menus;
        }

        /// <summary>
        /// Obtiene el menú para un perfil específico.
        /// </summary>
        /// <param name="idPerfil">
        /// Clave del perfil
        /// </param>
        /// <returns>
        /// Lista de menús para el perfil
        /// </returns>
        public List<Menu> GetMenusByProfile(int idPerfil)
        {
            Perfil perfil = _perfilDA.GetById(idPerfil);
            List<Menu> menus = perfil.Menus.ToList();
            return menus;
        }
    }

}
