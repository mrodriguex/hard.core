using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace HARD.CORE.OBJ
{
    public class Usuario
    {
        private string _claveUsuario;
        //private TipoUsuario _tipoUsuario;
        private Perfil _perfilActivo;
        private List<Perfil> _perfiles;
        private List<Empresa> _empresas;
        private Empresa _empresaActivo;
        private string _nombre;
        private string _apellidos;
        private string _apellidoPaterno;
        private string _apellidoMaterno;

        public string ClaveUsuario
        {
            get
            {
                if (_claveUsuario == null) _claveUsuario = "";
                return _claveUsuario;
            }
            set
            {
                _claveUsuario = value;
            }
        }
        public int NumeroEmpleado { get; set; }
        public bool esActive
        {
            get; set;
        }

        public Perfil PerfilActivo
        {
            get
            {
                if (_perfilActivo == null)
                {
                    _perfilActivo = new Perfil();
                }
                return _perfilActivo;
            }
            set
            {
                _perfilActivo = value;
            }
        }
        public List<Perfil> Perfiles
        {
            get
            {
                if (_perfiles == null)
                {
                    _perfiles = new List<Perfil>();
                }
                return _perfiles;
            }
            set
            {
                _perfiles = value;
            }
        }

        public Empresa EmpresaActivo
        {
            get
            {
                if (_empresaActivo == null)
                {
                    _empresaActivo = new Empresa();
                }
                return _empresaActivo;
            }
            set
            {
                _empresaActivo = value;
            }
        }

        public List<Empresa> Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    _empresas = new List<Empresa>();
                }
                return _empresas;
            }
            set
            {
                _empresas = value;
            }
        }
        public string NombreCompleto
        {
            get
            {
                return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";
            }
        }
        public string Nombre
        {
            get
            {
                if (_nombre == null) _nombre = "";
                return _nombre;
            }
            set
            {
                _nombre = value;
            }
        }
        public string Apellidos
        {
            get
            {
                if (_apellidos == null) _apellidos = "";
                return _apellidos;
            }
            set
            {
                _apellidos = value;
            }
        }
        public string ApellidoPaterno
        {
            get
            {
                if (_apellidoPaterno == null) _apellidoPaterno = "";
                return _apellidoPaterno;
            }
            set
            {
                _apellidoPaterno = value;
            }
        }
        public string ApellidoMaterno
        {
            get
            {
                if (_apellidoMaterno == null) _apellidoMaterno = "";
                return _apellidoMaterno;
            }
            set
            {
                _apellidoMaterno = value;
            }
        }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int NumeroIntentos { get; set; }
        public bool Bloqueado { get; set; }
        public bool CambioContrasena { get; set; }
        public bool Estatus { get; set; }
        public string Fotografia { get; set; }
        public string ClaveUsuarioPorAusencia { get; set; }
        public string NombreUsuarioPorAusencia { get; set; }
        public string ClaveUsuarioAlta { get; set; }
        public DateTime FechaAlta { get; set; }
        public string ClaveUsuarioUltimaActualizacion { get; set; }

        public DateTime FechaUltimaActualizacion { get; set; }

        public Usuario()
        {
        }

    }

}
