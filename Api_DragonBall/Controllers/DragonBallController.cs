using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Class.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

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
        public async Task<DragonBallResponse> GetCharacterPage(int page = 1, int limit = 12)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://dragonball-api.com/api/characters?page={page}&limit={limit}");

                if (response.IsSuccessStatusCode)
                {
                    var characterJson = await response.Content.ReadAsStringAsync();
                    var characterResponse = JsonConvert.DeserializeObject<DragonBallResponse>(characterJson);

                    return characterResponse;
                }
                else
                {
                    return null; // O lanzar una excepción según tu lógica de manejo de errores
                }
            }
            catch (HttpRequestException)
            {
                return null; // O lanzar una excepción según tu lógica de manejo de errores
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
                    return character;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception("Error al obtener el personaje");
                }
            }
            catch (HttpRequestException)
            {
                // Manejar el error de conexión de alguna otra manera si es necesario
                throw new HttpRequestException("Error al conectarse al servicio");
            }
        }

        [HttpGet("{search}")]
        public async Task<List<Character>> SearchCharacters(string search)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://dragonball-api.com/api/characters?name={search}");

                if (response.IsSuccessStatusCode)
                {
                    var characterJson = await response.Content.ReadAsStringAsync();
                    var characterResponse = JsonConvert.DeserializeObject<List<Character>>(characterJson);



                    return characterResponse;
                }
                else
                {
                    return null; // O lanzar una excepción según tu lógica de manejo de errores
                }
            }
            catch (HttpRequestException)
            {
                return null; // O lanzar una excepción según tu lógica de manejo de errores
            }
        }

    }
}
