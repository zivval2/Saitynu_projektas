using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class TimeController : ControllerBase
    {
        private readonly ClientContext _context;

        public TimeController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/Time
        [HttpGet]
        public IEnumerable<Time> GetTimes()
        {
            return _context.Times;
        }

        // GET: api/Time/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTime([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var time = await _context.Times.FindAsync(id);

            if (time == null)
            {
                return NotFound();
            }

            return Ok(time);
        }

        [Authorize(Roles = Role.Artist)]
        // PUT: api/Time/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTime([FromRoute] int id, [FromBody] Time time)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var idClaim = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var artist = await _context.Users.FindAsync(time.ArtistId);
            if (artist == null || artist.Role != Role.Artist || idClaim.Value != time.ArtistId.ToString())
            {
                return Unauthorized();
            }

            if (id != time.TimeId)
            {
                return BadRequest();
            }

            _context.Entry(time).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeExists(id))
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

        // POST: api/Time
        [Authorize(Roles = Role.Artist)]
        [HttpPost]
        public async Task<IActionResult> PostTime([FromBody] Time time)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var idClaim = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var artist = await _context.Users.FindAsync(time.ArtistId);
            if (artist == null || artist.Role != Role.Artist || idClaim.Value != time.ArtistId.ToString())
            {
                return Unauthorized();
            }
            time.IsUsed = false;
            _context.Times.Add(time);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTime", new { id = time.TimeId }, time);
        }

        // DELETE: api/Time/5
        [Authorize(Roles = Role.Artist)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTime([FromRoute] int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var idClaim = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var time = await _context.Times.FindAsync(id);
            if (time == null)
            {
                return NotFound();
            }
            if(time.ArtistId.ToString() != idClaim.Value)
            {
                return Unauthorized();
            }

            _context.Times.Remove(time);
            await _context.SaveChangesAsync();

            return Ok(time);
        }

        private bool TimeExists(int id)
        {
            return _context.Times.Any(e => e.TimeId == id);
        }
    }
}