using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReadingClubSystem.Infrastructure.Interfaces;

namespace ReadingClubSystem.Infrastructure.Services
{
    public class LibroService : ILibroService
    {
        private readonly HttpClient _httpClient;

        public LibroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7146/api/Libro/");
        }

        public async Task<IEnumerable<Libro>> GetAllLibrosAsync()
        {
            var response = await _httpClient.GetAsync("listado/");
            if (response.IsSuccessStatusCode)
            {
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var libros = await response.Content.ReadFromJsonAsync<IEnumerable<Libro>>(opciones);
                return libros;
            }
            return new List<Libro>();
        }

        public async Task<Libro> GetLibroByIdAsync(int id)
        {
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return await _httpClient.GetFromJsonAsync<Libro>($"detalle/{id}", opciones);
        }

        public async Task AddLibroAsync(Libro libro)
        {
            var response = await _httpClient.PostAsJsonAsync("crear", libro);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }

        public async Task UpdateLibroAsync(Libro libro)
        {
            var response = await _httpClient.PutAsJsonAsync($"actualizar/{libro.Id}", libro);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }

        public async Task DeleteLibroAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"eliminar/{id}");
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }
    }
}
