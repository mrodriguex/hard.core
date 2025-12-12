using Asp.Versioning;
using HARD.CORE.API.Helpers;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG;
using HARD.CORE.NEG.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
namespace HARD.CORE.API.Controllers.Base
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path

    public abstract class BaseController : ControllerBase
    {

        /// <summary>
        /// Gets the unique key identifying the user from the JWT token.
        /// </summary>
        public string ClaveUsuario
        {
            get
            {
                string? token = Request.Headers["Authorization"];
                return JwtAuthenticateHelper.GetUsernameFromToken(token ?? "");
            }
        }

    }

}

