using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IMenuDA
    {
        Menu Obtener(int claveMenu);
        List<Menu> ObtenerTodos(bool? estatus = null, int? clavePerfil = null, string claveUsuario = null);
        List<Menu> ObtenerMenu_Usuario(string claveUsuario, int clavePerfil);
        List<Menu> ObtenerMenu_Perfil(int clavePerfil);
        public bool ConfigurarMenu_Perfil(int clavePerfil, List<Menu> menus);
        int Insertar(Menu Menu);
        bool Actualizar(Menu Menu);
    }
}
