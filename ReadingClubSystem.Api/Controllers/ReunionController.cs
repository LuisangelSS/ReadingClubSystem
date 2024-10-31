using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ReadingClubSystem.Domain.ViewModels;

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
        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Reunion>>> GetReuniones()
        {
            var reuniones = await _context.Reuniones
                .Include(r => r.Club)
                .ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(reuniones, options);
        }

        // Método para obtener una reunión específica por ID
        [HttpGet("detalle/{id}")]
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
        [HttpPost("crear")]
        public async Task<ActionResult<Reunion>> PostReunion(ReunionDto reunionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reunion = new Reunion
            {
                Tema = reunionDto.Tema,
                ClubId = reunionDto.ClubId,
                Fecha = reunionDto.Fecha
            };

            _context.Reuniones.Add(reunion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReunion), new { id = reunion.Id }, reunion);
        }

        // Método para actualizar una reunión existente
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutReunion(int id, ReunionDto reunionDto)
        {


            if (id != reunionDto.Id)
            {
                return BadRequest();
            }

            var reunion = await _context.Reuniones.FindAsync(id);
            if (reunion == null)
            {
                return NotFound();
            }

            reunion.ClubId = reunionDto.ClubId;
            reunion.Fecha = reunionDto.Fecha;
            reunion.Tema = reunionDto.Tema;

            _context.Entry(reunion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReunionExists(id))
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
        private bool ReunionExists(int id)
        {
            return _context.Reuniones.Any(e => e.Id == id);
        }


        // Método para eliminar una reunión
        [HttpDelete("eliminar/{id}")]
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
