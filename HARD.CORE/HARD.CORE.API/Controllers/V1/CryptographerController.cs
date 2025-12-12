using Asp.Versioning;
using HARD.CORE.API.Controllers.Base;
using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HARD.CORE.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class CryptographerController : BaseController
    {

        private readonly ICryptographer _cryptographer;

        public CryptographerController(ICryptographer cryptographer)
        {
            _cryptographer = cryptographer;
        }

        [HttpGet("CreateHash")]
        public IActionResult CreateHash([FromQuery] string? plainText = null)
        {
            var webResult = new WebResultModel<string>();
            try
            {
                if (string.IsNullOrEmpty(plainText))
                {
                    webResult.Message = "Error en el modelo recibido";
                    webResult.Errors.Add("El campo plainText es requerido");
                    webResult.Success = false;
                }
                else
                {
                    string decodedPlainText = Uri.UnescapeDataString(plainText);
                    webResult.Data = _cryptographer.CreateHash(algorithmName: "SHA512CryptoServiceProvider", plainText: decodedPlainText);
                    webResult.Success = true;
                }
            }
            catch (Exception ex)
            {
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        [HttpGet("CompareHash")]
        public IActionResult CompareHash([FromQuery] string? plainText = null, [FromQuery] string? hash = null)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                if (string.IsNullOrEmpty(plainText))
                {
                    webResult.Message = "Error en el modelo recibido";
                    webResult.Errors.Add("El campo plainText es requerido");
                    webResult.Success = false;
                }
                else if (string.IsNullOrEmpty(hash))
                {
                    webResult.Message = "Error en el modelo recibido";
                    webResult.Errors.Add("El campo hash es requerido");
                    webResult.Success = false;
                }
                else
                {
                    string decodedPlainText = Uri.UnescapeDataString(plainText);
                    string decodedHash = Uri.UnescapeDataString(hash);
                    webResult.Data = _cryptographer.CompareHash(algorithmName: "SHA512CryptoServiceProvider", plainText: decodedPlainText, hash: decodedHash);
                    webResult.Success = true;
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
