using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ReadingClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReunionController : ControllerBase
    {
        private readonly ReadingClubContext _context;

        public ReunionController(ReadingClubContext context)
        {
            _context = context;
        }

        // Método para obtener todas las reuniones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reunion>>> GetReuniones()
        {
            return await _context.Reuniones.ToListAsync();
        }

        // Método para obtener una reunión específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Reunion>> GetReunion(int id)
        {
            var reunion = await _context.Reuniones.FindAsync(id);

            if (reunion == null)
            {
                return NotFound();
            }

            return reunion;
        }

        // Método para crear una nueva reunión
        [HttpPost]
        public async Task<ActionResult<Reunion>> PostReunion(Reunion reunion)
        {
            _context.Reuniones.Add(reunion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReunion", new { id = reunion.Id }, reunion);
        }

        // Método para actualizar una reunión existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReunion(int id, Reunion reunion)
        {
            if (id != reunion.Id)
            {
                return BadRequest();
            }

            _context.Entry(reunion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reuniones.Any(e => e.Id == id))
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

        // Método para eliminar una reunión
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReunion(int id)
        {
            var reunion = await _context.Reuniones.FindAsync(id);
            if (reunion == null)
            {
                return NotFound();
            }

            _context.Reuniones.Remove(reunion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
