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
    public class ServiceController : ControllerBase
    {
        private readonly ClientContext _context;

        public ServiceController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        public IEnumerable<Service> GetServices()
        {
            return _context.Services;
        }


        // GET: api/Service/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _context.Services
                        .Include(i => i.ArtistId)
                        .FirstOrDefaultAsync(i => i.ServiceId == id);

            //var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Service/5
        [Authorize(Roles = Role.Artist)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService([FromRoute] int id, [FromBody] Service service)
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
            var artist = await _context.Users.FindAsync(service.ArtistId);
            if (artist == null || artist.Role != Role.Artist || idClaim.Value != service.ArtistId.ToString())
            {
                return Unauthorized();
            }

            if (id != service.ServiceId)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Service
        [Authorize(Roles = Role.Artist)]
        [HttpPost]
        public async Task<IActionResult> PostService([FromBody] Service service)
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
            var artist = await _context.Users.FindAsync(service.ArtistId);
            if (artist == null || artist.Role != Role.Artist || idClaim.Value != service.ArtistId.ToString())
            {
                return Unauthorized();
                //return BadRequest("pirma salyga" + service.ArtistId);
            }
            //if(artist.Role != Role.Artist)
            //{
            //    return BadRequest("antra salyga");
            //}
            //if(idClaim.Value != service.ArtistId.ToString())
            //{
            //    return BadRequest("trecia salyga");
            //}

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.ServiceId }, service);
        }

        // DELETE: api/Service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService([FromRoute] int id)
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

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            if (service.ArtistId.ToString() != idClaim.Value)
            {
                return Unauthorized();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(service);
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceId == id);
        }
    }
}