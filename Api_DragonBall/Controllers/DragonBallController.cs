using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Class.Models;
using System.Net;

namespace Api_DragonBall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DragonBallController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public DragonBallController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("{page},{limit}")]
        public async Task<IActionResult> GetCharacterPage(int page = 1, int limit = 10)
        {
            try
            {
                // Hacer la solicitud HTTP al servicio web
                var response = await _httpClient.GetAsync($"https://dragonball-api.com/api/characters?page={page}&limit={limit}");

                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar el contenido JSON de la respuesta
                    var characterJson = await response.Content.ReadAsStringAsync();
                    var character = JsonConvert.DeserializeObject<DragonBallResponse>(characterJson);

                    return Ok(character);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException)
            {
                return StatusCode(500, "Error al conectarse al servicio"); // Manejar el error de conexión de alguna otra manera
            }
        }


        [HttpGet("{id}")]
        public Character ObtenerPersonaje(int id)
        {
            try
            {
                var response = _httpClient.GetAsync($"https://dragonball-api.com/api/characters/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar el contenido JSON de la respuesta
                    var characterJson = response.Content.ReadAsStringAsync().Result;
                    var character = JsonConvert.DeserializeObject<Character>(characterJson);
                    Console.WriteLine("llegó acá 1");
                    return character;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("llegó acá 2");

                    return null; 
                }
                else
                {
                    Console.WriteLine("llegó acá 3");
                    throw new Exception("Error al obtener el personaje");
                }
            }
            catch (HttpRequestException)
            {
                // Manejar el error de conexión de alguna otra manera si es necesario
                throw new HttpRequestException("Error al conectarse al servicio");
            }
        }




    }
}
