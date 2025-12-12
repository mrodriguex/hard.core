using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HARD.CORE.API.Helpers
{
    /// <summary>
    /// Helper class for JWT authentication.
    /// </summary>
    public static class JwtAuthenticateHelper
    {

        /// <summary>
        /// Generates a JSON Web Token (JWT) for the specified user.
        /// </summary>
        /// <param name="username">
        /// The username of the user.</param>
        /// <param name="tokenDuration">
        /// The duration in minutes for which the token is valid.
        /// </param>
        /// <param name="jwtPrivKey">The private key used to sign the token.</param>
        /// <returns>
        /// The generated JWT token as a string.
        /// </returns>
        public static string GenerateJwtToken(string username, int tokenDuration, string jwtPrivKey)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtPrivKey ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenDuration),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Extracts the username from the JWT token in the Authorization header.
        /// </summary>
        /// <param name="authHeader">
        /// The Authorization header containing the JWT token.</param>
        /// <returns>The username extracted from the token, or an error message.</returns>
        public static string GetUsernameFromToken(string authHeader)
        {
            string claveUsuario = "";
            try
            {
                if (authHeader != null && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(token);

                    // Extract username from claims
                    var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    claveUsuario = usernameClaim != null ? usernameClaim.Value : "";
                }
            }
            catch
            {
                Console.WriteLine("Error occurred while parsing JWT token.");
            }
            return claveUsuario;
        }

    }
}
