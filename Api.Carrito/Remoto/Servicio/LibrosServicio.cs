using Api.Carrito.Remoto.Interface;
using Api.Carrito.Remoto.Modelo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Carrito.Remoto.Servicio
{
    public class LibrosServicio : ILibroServicio
    {
        private readonly IHttpClientFactory _httpClient;

        private readonly ILogger<LibrosServicio> _logger;


        public LibrosServicio(IHttpClientFactory httpClient, ILogger<LibrosServicio> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemoto libro, string errorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemoto>(contenido, options);
                    return (true, resultado, null);
                }
                return (false, null, response.ReasonPhrase);
            } catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            };
        }
    }
}
