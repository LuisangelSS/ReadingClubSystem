using System.Net.Http;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Json;
using Azure;
using System.Text.Json.Serialization;
using System.Text.Json;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;

    public UsuarioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7146/api/Usuario/"); 
    }
    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
    {
        var response = await _httpClient.GetAsync("listado/");
        if (response.IsSuccessStatusCode)
        {
            var usuarios = await response.Content.ReadFromJsonAsync<IEnumerable<Usuario>>();
            return usuarios;
        }
        return new List<Usuario> ();
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        return await _httpClient.GetFromJsonAsync<Usuario>($"detalle/{id}", options);
    }


    //crear
    public async Task AddUsuarioAsync(Usuario usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("crear", usuario);
        response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
    }


    public async Task UpdateUsuarioAsync(Usuario usuario)
    {
        var response = await _httpClient.PutAsJsonAsync($"actualizar/{usuario.Id}", usuario);
        response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"eliminar/{id}");
        response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
    }




}
