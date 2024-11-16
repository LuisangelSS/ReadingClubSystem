using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReadingClubSystem.Domain.ViewModels;
using ReadingClubSystem.Infrastructure.Interfaces;

namespace ReadingClubSystem.Infrastructure.Services
{
    public class ReunionService : IReunionService
    {
        private readonly HttpClient _httpClient;

        public ReunionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7146/api/Reunion/");
        }

        public async Task<IEnumerable<Reunion>> GetAllReunionesAsync()
        {
            var response = await _httpClient.GetAsync("listado/");
            if (response.IsSuccessStatusCode)
            {
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var reuniones = await response.Content.ReadFromJsonAsync<IEnumerable<Reunion>>(opciones);
                return reuniones;
            }
            return new List<Reunion>();
        }

        public async Task<Reunion> GetReunionByIdAsync(int id)
        {
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return await _httpClient.GetFromJsonAsync<Reunion>($"detalle/{id}", opciones);
        }

        public async Task AddReunionAsync(ReunionDto reunionDto)
        {
            var response = await _httpClient.PostAsJsonAsync("crear", reunionDto);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }

        public async Task UpdateReunionAsync(Reunion reunion)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var response = await _httpClient.PutAsJsonAsync($"actualizar/{reunion.Id}", reunion, options);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }

        public async Task DeleteReunionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"eliminar/{id}");
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa

        }
    }
}
