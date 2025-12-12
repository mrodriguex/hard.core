using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Helpers;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HARD.CORE.API.Controllers.V1
{
    /// <summary>
    /// Controller for user authentication.
    /// </summary>
    /// <remarks>
    /// This controller handles user login and token generation.
    /// </remarks>  

    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path    
    public class AuthController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioB _usuarioB;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="config">
        /// The configuration settings for the application.
        /// </param>
        /// <param name="usuarioB">
        /// The user business logic layer.
        /// </param>
        public AuthController(IConfiguration config, IUsuarioB usuarioB)
        {
            _config = config;
            _usuarioB = usuarioB;
        }

        /// <summary>
        /// Handles user login requests.
        /// </summary>
        /// <param name="login">The login credentials provided by the user.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a <c>WebResultModel&lt;string&gt;</c> object with the result of the authentication process.
        /// If authentication is successful, returns a JWT token; otherwise, returns error messages indicating the reason for failure.
        /// </returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var webResult = new WebResultModel<string>();
            webResult.Success = false;
            webResult.Message = "Error de autenticación";

            try
            {
                Usuario usuario = _usuarioB.Obtener(claveUsuario: login.Username);

                if (string.IsNullOrEmpty(usuario.ClaveUsuario))
                {
                    webResult.Errors.Add("Usuario no existe en el sistema");
                }
                else if (usuario.Bloqueado)
                {
                    webResult.Errors.Add("Usuario bloqueado");
                }
                else if (!_usuarioB.AutenticarUsuario(claveUsuario: login.Username, password: login.Password))
                {
                    webResult.Errors.Add("Credenciales son incorrectas");
                }
                else
                {
                    int tokenDuration = 60; //Default value
                    int.TryParse(_config["Jwt:Duration"], out tokenDuration);   //Try parse token duration from appsettings.json, otherwise keep default value
                    var jwtPrivKey = _config["Jwt:Key"] ?? "";
                    webResult.Data = JwtAuthenticateHelper.GenerateJwtToken(login.Username, tokenDuration, jwtPrivKey);
                    webResult.Success = true;
                    webResult.Message = "Inicio de sesión exitoso";
                }
            }
            catch (Exception ex)
            {
                webResult.Errors.Add(ex.Message);
            }

            return Ok(webResult);
        }

    }

}
