using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Web.Services.Interfaces;
using ReadingClubSystem.Domain.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace ReadingClubSystem.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(HttpClient httpClient, IUsuarioService usuarioService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7146/api/Usuario/");
            _usuarioService = usuarioService;

        }

        // Método para listar todos los usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            
            return View(usuarios);

        }

        // Método para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Método para crear un nuevo usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _usuarioService.AddUsuarioAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // Método para mostrar el formulario de edición
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();
            return View(usuario);
        }

        // Método para actualizar un usuario existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _usuarioService.UpdateUsuarioAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // Método para confirmar la eliminación
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
