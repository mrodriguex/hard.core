using Asp.Versioning;

using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

namespace HARD.CORE.API.Controllers.V2
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioB _usuarioB;

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
                    webResult.Data = GenerateJwtToken(login.Username);
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

        /// <summary>
        /// Generates a JSON Web Token (JWT) for the specified user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
        };

            int tokenDuration = 60; //Default value
            int.TryParse(_config["Jwt:Duration"], out tokenDuration);   //Try parse token duration from appsettings.json, otherwise keep default value

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenDuration),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
