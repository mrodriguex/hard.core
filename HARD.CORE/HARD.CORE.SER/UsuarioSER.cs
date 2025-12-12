using HARD.CORE.OBJ;
using HARD.CORE.SER.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace HARD.CORE.SER
{
    public class UsuarioSER
    {
        #region "Singleton"

        private static UsuarioSER instance = null;

        private static object mutex = new object();
        private UsuarioSER()
        {
        }

        public static UsuarioSER GetInstance()
        {

            if (instance == null)
            {
                lock ((mutex))
                {
                    instance = new UsuarioSER();
                }
            }
            return instance;
        }

        #endregion

        public Usuario Obtener(string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<Usuario>(endPoint: "api/v1/Usuario/Obtener", query: $"claveUsuario={encodedClaveUsuario}");
            return result;
        }

        public List<Usuario> ObtenerTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<List<Usuario>>(endPoint: "api/v1/Usuario/ObtenerTodos");
            return result;
        }
        public List<Usuario> ObtenerUsuariosDirectorioActivo()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<List<Usuario>>(endPoint: "api/v1/Usuario/ObtenerUsuariosDirectorioActivo");
            return result;
        }
        public List<Usuario> ObtenerUsuariosDirectorioActivoTodos()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<List<Usuario>>(endPoint: "api/v1/Usuario/ObtenerUsuariosDirectorioActivoTodos");
            return result;
        }

        public string ObtenerUsuarioSugerido(string apellidoPaterno, string apellidoMaterno, string nombres)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<string>(endPoint: "api/v1/Usuario/ObtenerUsuarioSugerido", query: $"apellidoPaterno={apellidoPaterno}&apellidoMaterno={apellidoMaterno}&nombres={nombres}");
            return result;
        }


        public void Insertar(Usuario usuario)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            httpClientManager.PostWebResult<object>(obj: usuario, endPoint: "api/v1/Usuario/Insertar");
        }

        public void Actualizar(Usuario usuario)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            httpClientManager.PutWebResult<object>(obj: usuario, endPoint: "api/v1/Usuario/Actualizar");
        }





        //public DataTable ObtenerUsuariosDirectorioActivo()
        //{
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
        //    return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Usuario/ObtenerUsuariosDirectorioActivo");
        //}

        //public DataTable ObtenerNoRegistradosDirectorioActivo()
        //{
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
        //    return httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Usuario/ObtenerNoRegistradosDirectorioActivo");
        //}

        public bool Desbloquea(string claveUsuario)
        {

            Usuario usuario = new Usuario();
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            usuario.ClaveUsuario = encodedClaveUsuario;
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.PutWebResult<bool>(obj: claveUsuario, endPoint: "api/v1/Usuario/Desbloquear");
            return result;
        }

        public bool AutenticarUsuario(string claveUsuario, string password)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            Login login = new Login();
            login.Username = encodedClaveUsuario;
            login.Password = password;
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            // En el cliente
            //       bool result = httpClientManager.GetWebResult<bool>(
            //endPoint: $"api/v1/Usuario/Autenticar?Username={Uri.EscapeDataString(login.Username)}&Password={Uri.EscapeDataString(login.Password)}");
            bool result = httpClientManager.PostWebResult<bool>(obj: login, endPoint: "api/v1/Usuario/Autenticar");
            //bool result = httpClientManager.GetWebResult<bool>(endPoint: "api/v1/Usuario/Autenticar", query: $"claveUsuario={encodedClaveUsuario}&password={encodedPassword}");
            return result;
        }

        //public bool PuedeCambiarContrasena(string claveUsuario)
        //{
        //    string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl);
        //    var result = httpClientManager.GetWebResult<bool>(endPoint: "api/v1/Usuario/PuedeCambiarContrasena", query: $"claveUsuario={encodedClaveUsuario}");
        //    return result;
        //}

        public bool ExisteUsuario(string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl);
            var result = httpClientManager.GetWebResult<bool>(endPoint: "api/v1/Usuario/ExisteUsuario", query: $"claveUsuario={encodedClaveUsuario}");
            return result;
        }

        //public DataTable ObtenerCorreosCambioContrasena(string claveUsuario)
        //{
        //    string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl);
        //    var result = httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Usuario/ObtenerCorreosCambioContrasena", query: $"claveUsuario={encodedClaveUsuario}");
        //    return result;
        //}

        public void RegistrarActividad(string claveUsuario, int tipoRegistro)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            httpClientManager.GetWebResult<object>(endPoint: "api/v1/Usuario/RegistrarActividad", query: $"claveUsuario={encodedClaveUsuario}&tipoRegistro={tipoRegistro}");
            return;
        }

        //public bool ActualizandoInformacion()
        //{
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
        //    var result = httpClientManager.GetWebResult<bool>(endPoint: "api/v1/Usuario/ActualizandoInformacion");
        //    return result;
        //}

        //public void ActualizaIntento(string claveUsuario)
        //{
        //    string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
        //    httpClientManager.GetWebResult(endPoint: "api/v1/Usuario/ActualizaIntento", query: $"claveUsuario={encodedClaveUsuario}");
        //    return;
        //}

        //public void EnviarCambioContrasena(string claveUsuario, int? claveContacto)
        //{
        //    string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl);
        //    httpClientManager.GetWebResult(endPoint: "api/v1/Usuario/EnviarCambioContrasena",query: $"claveUsuario={encodedClaveUsuario}&claveContacto={claveContacto}");
        //    return;
        //}

        //public DataTable ObtenerTodos()
        //{
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
        //    var result = httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Usuario/ObtenerTodos");
        //    return result;
        //}

        //public string ObtenerUsuarioSugerido(string apellidoPaterno, string apellidoMaterno, string nombres)
        //{
        //    string encodedApellidoPaterno = Uri.EscapeDataString(apellidoPaterno);
        //    string encodedApellidoMaterno = Uri.EscapeDataString(apellidoMaterno);
        //    string encodedNombres = Uri.EscapeDataString(nombres);
        //    HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
        //    var result = httpClientManager.GetWebResult<string>(endPoint: "api/v1/Usuario/ObtenerUsuarioSugerido", query: $"apellidoPaterno={encodedApellidoPaterno}&apellidoMaterno={encodedApellidoMaterno}&nombres={encodedNombres}");
        //    return result;
        //}

        public void InsertarUsuario(Usuario usuario)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            httpClientManager.PostWebResult(obj: usuario, endPoint: "api/v1/Usuario/Insertar");
            return;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            httpClientManager.PutWebResult(obj: usuario, endPoint: "api/v1/Usuario/Actualizar");
            return;
        }

        public bool ActualizaContrasena(string claveUsuario, string contrasena)
        {
            string claveUsuarioEncoded = Uri.EscapeDataString(claveUsuario);
            string contrasenaEncoded = Uri.EscapeDataString(contrasena);
            Login login = new Login();
            login.Username = claveUsuario;
            login.Password = contrasena;
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            bool result = httpClientManager.PutWebResult<bool>(obj: login, endPoint: "api/v1/Usuario/ActualizaContrasena");
            return result;
        }

        public List<BitacoraAcessos> ObtenerActividad()
        {
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<List<BitacoraAcessos>>(endPoint: "api/v1/Usuario/ObtenerActividad");
            return result;
        }

        public DataTable ObtenerDetalleActividad(string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<DataTable>(endPoint: "api/v1/Usuario/ObtenerDetalleActividad", query: $"claveUsuario={encodedClaveUsuario}");
            return result;
        }

        public bool EnviarAvisoCambioContrasena(string claveUsuario)
        {
            string encodedClaveUsuario = Uri.EscapeDataString(claveUsuario);
            HttpClientManager httpClientManager = new HttpClientManager(urlBase: ConfigurationHelper.BackendApiUrl, token: TokenHelper.Token);
            var result = httpClientManager.GetWebResult<bool>(endPoint: "api/v1/Usuario/EnviarAvisoCambioContrasena", query: $"claveUsuario={encodedClaveUsuario}");
            return result;
        }
    }
}
