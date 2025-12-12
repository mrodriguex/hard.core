using Asp.Versioning;

using HARD.CORE.API.Models.V1;
using HARD.CORE.NEG;
using HARD.CORE.NEG.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace HARD.CORE.API.Controllers.V2
{
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path

    public class ArchivoController : ControllerBase
    {

        private IArchivoB _archivoB;

        public ArchivoController(IArchivoB archivoB)
        {
            _archivoB = archivoB;
        }

        // [AllowAnonymous]
        // [HttpGet("ObtenerPoliticasPrivacidad")]
        // public IActionResult ObtenerPoliticasPrivacidad()
        // {
        //     var webResult = new WebResultModel<byte[]>();
        //     try
        //     {
        //         webResult.Data = _archivoB.ObtenerPoliticasPrivacidad();
        //         webResult.Message = "Archivo recuperado exitosamente";
        //         webResult.Success = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         webResult.Message = "Error en la recuperación del archivo.";
        //         webResult.Errors.Add(ex.Message);
        //     }
        //     return Ok(webResult);
        // }

        [HttpGet("ObtenerDocumentoTanqueSTPS")]
        public IActionResult ObtenerDocumentoTanqueSTPS([FromQuery,Required] int claveCliente, [FromQuery,Required] int numeroEconomico)
        {
            var webResult = new WebResultModel<byte[]>();
            try
            {
                webResult.Data = _archivoB.ObtenerDocumentoTanqueSTPS(claveCliente, numeroEconomico);
                webResult.Message = "Archivo recuperado exitosamente";
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                webResult.Message = "Error en la recuperación del archivo.";
                webResult.Errors.Add(ex.Message);
            }
            return Ok(webResult);
        }

        // [HttpGet("ObtenerCertificadoCalidad")]
        // public IActionResult ObtenerCertificadoCalidad([FromQuery, Required] int folioRemision, [FromQuery, Required] string serieRemision)
        // {
        //     var webResult = new WebResultModel<byte[]>();
        //     try
        //     {
        //         string decodedSerieRemision = Uri.UnescapeDataString(serieRemision);
        //         webResult.Data = _archivoB.ObtenerCertificadoCalidad(folioRemision, decodedSerieRemision);
        //         webResult.Message = "Archivo recuperado exitosamente";
        //         webResult.Success = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         webResult.Message = "Error en la recuperación del archivo.";
        //         webResult.Errors.Add(ex.Message);
        //     }
        //     return Ok(webResult);
        // }
        // [HttpGet("ObtenerFotoNivelInicial")]
        // public IActionResult ObtenerFotoNivelInicial([FromQuery, Required] int folioRemision, [FromQuery, Required] string serieRemision)
        // {
        //     var webResult = new WebResultModel<byte[]>();
        //     try
        //     {
        //         string decodedSerieRemision = Uri.UnescapeDataString(serieRemision);
        //         webResult.Data = _archivoB.ObtenerFotoNivelInicial(folioRemision, decodedSerieRemision);
        //         webResult.Message = "Archivo recuperado exitosamente";
        //         webResult.Success = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         webResult.Message = "Error en la recuperación del archivo.";
        //         webResult.Errors.Add(ex.Message);
        //     }
        //     return Ok(webResult);
        // }
        // [HttpGet("ObtenerFotoNivelFinal")]
        // public IActionResult ObtenerFotoNivelFinal([FromQuery, Required] int folioRemision, [FromQuery, Required] string serieRemision)
        // {
        //     var webResult = new WebResultModel<byte[]>();
        //     try
        //     {
        //         string decodedSerieRemision = Uri.UnescapeDataString(serieRemision);
        //         webResult.Data = _archivoB.ObtenerFotoNivelFinal(folioRemision, decodedSerieRemision);
        //         webResult.Message = "Archivo recuperado exitosamente";
        //         webResult.Success = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         webResult.Message = "Error en la recuperación del archivo.";
        //         webResult.Errors.Add(ex.Message);
        //     }
        //     return Ok(webResult);
        // }
        // [HttpGet("ObtenerFotoPresionInicial")]
        // public IActionResult ObtenerFotoPresionInicial([FromQuery, Required] int folioRemision, [FromQuery, Required] string serieRemision)
        // {
        //     var webResult = new WebResultModel<byte[]>();
        //     try
        //     {
        //         string decodedSerieRemision = Uri.UnescapeDataString(serieRemision);
        //         webResult.Data = _archivoB.ObtenerFotoPresionInicial(folioRemision, decodedSerieRemision);
        //         webResult.Message = "Archivo recuperado exitosamente";
        //         webResult.Success = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         webResult.Message = "Error en la recuperación del archivo.";
        //         webResult.Errors.Add(ex.Message);
        //     }
        //     return Ok(webResult);
        // }
        // [HttpGet("ObtenerFotoPresionFinal")]
        // public IActionResult ObtenerFotoPresionFinal([FromQuery, Required] int folioRemision, [FromQuery, Required] string serieRemision)
        // {
        //     var webResult = new WebResultModel<byte[]>();
        //     try
        //     {
        //         string decodedSerieRemision = Uri.UnescapeDataString(serieRemision);
        //         webResult.Data = _archivoB.ObtenerFotoPresionFinal(folioRemision, decodedSerieRemision);
        //         webResult.Message = "Archivo recuperado exitosamente";
        //         webResult.Success = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         webResult.Message = "Error en la recuperación del archivo.";
        //         webResult.Errors.Add(ex.Message);
        //     }
        //     return Ok(webResult);
        // }
     
    }


}
