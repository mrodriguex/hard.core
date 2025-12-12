using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;

namespace HARD.CORE.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Version in the URL path
    public class ConfigController : Controller
    {
        private readonly string _filePath = "appsettings.json";

        // PUT: api/config/{key}
        [HttpPut("{key}")]
        public IActionResult UpdateAppSetting(string key, [FromBody] string newValue)
        {
            try
            {
                // Leer el archivo appsettings.json
                var json = System.IO.File.ReadAllText(_filePath);

                // Convertir el archivo a JObject
                var jsonObj = JObject.Parse(json);

                // Dividir la ruta del key en caso de ser anidado
                var sectionPath = key.Split(':');

                // Buscar la sección y modificarla
                var configSection = jsonObj;
                for (int i = 0; i < sectionPath.Length - 1; i++)
                {
                    var nextSection = configSection[sectionPath[i]];
                    if (nextSection == null || nextSection.Type != JTokenType.Object)
                    {
                        return BadRequest(new { message = $"Section '{sectionPath[i]}' not found or is not an object." });
                    }
                    configSection = (JObject)nextSection;
                }
                configSection[sectionPath.Last()] = newValue;

                // Guardar los cambios en el archivo appsettings.json
                System.IO.File.WriteAllText(_filePath, jsonObj.ToString());

                return Ok(new { message = $"Key '{key}' updated to '{newValue}' successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error updating appsettings.json: {ex.Message}" });
            }
        }

        // GET: api/config/{key}
        [HttpGet("{key}")]
        public IActionResult GetAppSetting(string key)
        {
            try
            {
                // Leer el archivo appsettings.json
                var json = System.IO.File.ReadAllText(_filePath);

                // Convertir el archivo a JObject
                var jsonObj = JObject.Parse(json);

                // Dividir la ruta del key en caso de ser anidado
                var sectionPath = key.Split(':');

                // Buscar la sección y modificarla
                var configSection = jsonObj;
                for (int i = 0; i < sectionPath.Length - 1; i++)
                {
                    var nextSection = configSection[sectionPath[i]];
                    if (nextSection == null || nextSection.Type != JTokenType.Object)
                    {
                        return BadRequest(new { message = $"Section '{sectionPath[i]}' not found or is not an object." });
                    }
                    configSection = (JObject)nextSection;
                }

                Dictionary<string, object> result = new Dictionary<string, object>();
                var value = configSection[sectionPath.Last()];
                if (value == null)
                {
                    return NotFound(new { message = $"Key '{key}' not found." });
                }
                result.Add(key, value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error reading appsettings.json: {ex.Message}" });
            }
        }
    }
}
