using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IEmpresaDA
    {
        List<Empresa> ObtenerTodos(int? clavePerfil = null, string claveUsuario = null);
        List<Empresa> ObtenerEmpresasUsuario(string claveUsuario = null);
        Empresa Obtener(int? claveEmpresa = null);
        bool InsertarEmpresaUsuario(Empresa empresa, string claveUsuario);
        bool EliminarEmpresasUsuario(string claveUsuario);
    }
}
