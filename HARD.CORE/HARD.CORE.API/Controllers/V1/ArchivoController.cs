using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.NEG.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HARD.CORE.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path

    public class ArchivoController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IAvisoB _avisoB;

        public ArchivoController(IConfiguration config, IAvisoB avisoB)
        {
            _config = config;
            _avisoB = avisoB;
        }

    }


}
