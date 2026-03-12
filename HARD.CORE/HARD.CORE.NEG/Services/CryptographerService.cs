using System;
using HARD.CORE.OBJ.Models;
using Microsoft.Extensions.Logging;

namespace HARD.CORE.NEG.Services
{
    public class CryptographerService
    {
        private readonly ICryptographerB _cryptographer;
        private readonly ILogger<CryptographerService> _logger;

        public CryptographerService(ICryptographerB cryptographer, ILogger<CryptographerService> logger)
        {
            _cryptographer = cryptographer;
            _logger = logger;
        }

        public WebResultModel<string> CreateHash(string? input)
        {
            var webResult = new WebResultModel<string>();
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    webResult.Message = "Error en el modelo recibido";
                    webResult.Errors.Add("El campo input es requerido");
                    return webResult;
                }
                string decodedPlainText = Uri.UnescapeDataString(input);
                webResult.Data = _cryptographer.CreateHash(input: decodedPlainText);
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el hash para el input: {Input}", input);
                webResult.Message = "Error al crear el hash.";
                webResult.Errors.Add(ex.Message);                
            }
            return webResult;
        }

        public WebResultModel<bool> CompareHash(string input, string hash)
        {
            var webResult = new WebResultModel<bool>();
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    webResult.Message = "Error en el modelo recibido";
                    webResult.Errors.Add("El campo input es requerido");
                    return webResult;
                }
                if (string.IsNullOrEmpty(hash))
                {
                    webResult.Message = "Error en el modelo recibido";
                    webResult.Errors.Add("El campo hash es requerido");
                    return webResult;
                }
                string decodedPlainText = Uri.UnescapeDataString(input);
                string decodedHash = Uri.UnescapeDataString(hash);
                webResult.Data = _cryptographer.CompareHash(input: decodedPlainText, hash: decodedHash);
                webResult.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al comparar el hash para el input: {Input} y hash: {Hash}", input, hash);
                webResult.Message = "Error al comparar el hash.";
                webResult.Errors.Add(ex.Message);
                webResult.Success = false;
            }
            return webResult;
        }
    }

}