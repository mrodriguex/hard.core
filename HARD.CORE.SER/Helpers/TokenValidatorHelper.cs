//using CRY.PCC.OBJ;

//using Newtonsoft.Json;

//using System;
//using System.IdentityModel.Tokens.Jwt;

//namespace CRY.PCC.SER.Helpers
//{
//    public static class TokenValidatorHelper
//    {

//        public static bool IsTokenExpired(string token)
//        {
//            var handler = new JwtSecurityTokenHandler();
//            var jwtToken = handler.ReadJwtToken(token);

//            var expiration = jwtToken.ValidTo; // Fecha de expiración en UTC
//            return expiration < DateTime.UtcNow; // Retorna true si está expirado
//        }

//    }
//}
