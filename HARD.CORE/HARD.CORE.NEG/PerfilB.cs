using HARD.CORE.DAT.Interfaces;
using HARD.CORE.OBJ;
using HARD.CORE.NEG.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HARD.CORE.NEG
{
    public class PerfilB : IPerfilB
    {

        IRepositoryBase<Perfil, BaseFilter, int> _perfilDA;
        IRepositoryBase<Usuario, BaseFilter, int> _usuarioDA;

        public PerfilB(IRepositoryBase<Perfil, BaseFilter, int> perfilDA, IRepositoryBase<Usuario, BaseFilter, int> usuarioDA)
        {
            _perfilDA = perfilDA;
            _usuarioDA = usuarioDA;
        }

        /// <summary>
        /// Obtains a profile by its unique key.
        /// </summary>
        /// <param name="idPerfil">The unique key identifying the profile.</param>
        /// <returns>The profile associated with the provided key.</returns>
        public Perfil GetById(int idPerfil)
        {
            return _perfilDA.GetById(idPerfil);
        }


        /// <summary>
        /// Obtains all profiles.
        /// </summary>
        /// <param name="pagedFilter">
        /// The filter and pagination information to apply.
        /// </param>
        /// <returns>A list of profiles matching the specified criteria.</returns>
        public IEnumerable<Perfil> GetAll(PagedFilter<BaseFilter> pagedFilter)
        {
            return _perfilDA.GetAll(pagedFilter).ToList();
        }

        /// <summary>
        /// Inserts a new profile.
        /// </summary>
        /// <param name="perfil">The profile to insert.</param>
        /// <returns>The unique key of the inserted profile.</returns>
        public int Add(Perfil perfil)
        {
            return _perfilDA.Add(perfil);
        }

        /// <summary>
        /// Updates an existing profile.
        /// </summary>
        /// <param name="perfil">The profile to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public bool Update(Perfil perfil)
        {
            return _perfilDA.Update(perfil);
        }

        /// <summary>
        /// Deletes a profile by its unique key.
        /// </summary>
        /// <param name="idPerfil">The unique key identifying the profile.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        public bool Delete(int idPerfil)
        {
            return _perfilDA.Delete(idPerfil);
        }

        /// <summary>
        /// Obtains the profiles associated with a specific user.
        /// </summary>
        /// <param name="idUsuario">The unique key identifying the user.</param>
        /// <returns>A list of profiles associated with the provided user key.</returns>
        public List<Perfil> GetUserProfiles(int idUsuario)
        {
            var user = _usuarioDA.GetById(idUsuario);
            if (user == null)
            {
                return new List<Perfil>();
            }
            return user.Perfiles.ToList();
        }

        public bool AssignProfileToUser(int idUsuario, int idPerfil)
        {
            Usuario usuario = _usuarioDA.GetById(idUsuario);
            Perfil perfil = _perfilDA.GetById(idPerfil);
            if (usuario == null || perfil == null)
            {
                return false;
            }

            var existingPerfil = usuario.Perfiles.FirstOrDefault(p => p.IdPerfil == idPerfil);
            if (existingPerfil != null)
            {
                return false;
            }

            usuario.Perfiles.Add(perfil);
            return _usuarioDA.Update(usuario);
        }

        public bool RemoveProfileFromUser(int idUsuario, int idPerfil)
        {
            Usuario usuario = _usuarioDA.GetById(idUsuario);
            Perfil perfil = _perfilDA.GetById(idPerfil);
            if (usuario == null || perfil == null)
            {
                return false;
            }
            var existingPerfil = usuario.Perfiles.FirstOrDefault(p => p.IdPerfil == idPerfil);
            if (existingPerfil == null)
            {
                return false;
            }
            usuario.Perfiles.Remove(existingPerfil);
            return _usuarioDA.Update(usuario);
        }
    }
}
