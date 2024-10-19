﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadingClubSystem.Api.Data;
using ReadingClubSystem.Domain.Entities;

namespace ReadingClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly ReadingClubContext _context;

        public ClubController(ReadingClubContext context)
        {
            _context = context;
        }

        // GET: api/Club
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Club>>> GetClubes()
        {
            return await _context.Clubes.ToListAsync();
        }

        // GET: api/Club/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(int id)
        {
            var club = await _context.Clubes.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            return club;
        }

        // POST: api/Club
        [HttpPost]
        public async Task<ActionResult<Club>> PostClub(Club club)
        {
            _context.Clubes.Add(club);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClub), new { id = club.Id }, club);
        }

        // PUT: api/Club/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(int id, Club club)
        {
            if (id != club.Id)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
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

        // DELETE: api/Club/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var club = await _context.Clubes.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubes.Remove(club);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClubExists(int id)
        {
            return _context.Clubes.Any(e => e.Id == id);
        }
    }
}