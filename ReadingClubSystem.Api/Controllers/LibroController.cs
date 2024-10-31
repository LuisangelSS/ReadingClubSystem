using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ReadingClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ReadingClubContext _context;

        public LibroController(ReadingClubContext context)
        {
            _context = context;
        }

        // Método para obtener todos los libros
        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        // Método para obtener un libro específico por ID
        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        // Método para crear un nuevo libro
        [HttpPost("crear")]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
        }

        // Método para actualizar un libro existente
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }
            _context.Entry(libro).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Libros.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // Método para eliminar un libro
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
