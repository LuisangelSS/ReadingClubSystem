using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Web.Services.Interfaces;
using ReadingClubSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ReadingClubSystem.Web.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;

        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        // Método para listar todos los libros
        public async Task<IActionResult> Index()
        {
            var libros = await _libroService.GetAllLibrosAsync();
            return View(libros);
        }

        // Método para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Método para crear un nuevo libro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                await _libroService.AddLibroAsync(libro);
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // Método para mostrar el formulario de edición
        public async Task<IActionResult> Edit(int id)
        {
            var libro = await _libroService.GetLibroByIdAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // Método para actualizar un libro existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                await _libroService.UpdateLibroAsync(libro);
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // Método para mostrar el formulario de eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var libro = await _libroService.GetLibroByIdAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // Método para confirmar la eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _libroService.DeleteLibroAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
