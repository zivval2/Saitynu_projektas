using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saitynu_projektas.Models;

namespace Saitynu_projektas.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ClientContext _context;

        public ArtistController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/Artist
        [HttpGet]
        public IEnumerable<Artist> GetArtists()
        {
            return _context.Artists;
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [HttpGet("{id}/services")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetArtistServices([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }
            var services =  _context.Services.Where(l => l.Artist.ArtistId == id);

            return Ok(services);
        }

        [HttpGet("{id}/times")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetArtistTimes([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }
            var times = _context.Times.Where(l => l.Artist.ArtistId == id);

            return Ok(times);
        }

        // PUT: api/Artist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist([FromRoute] int id, [FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artist
        [HttpPost]
        public async Task<IActionResult> PostArtist([FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.ArtistId }, artist);
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok(artist);
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistId == id);
        }
    }
}