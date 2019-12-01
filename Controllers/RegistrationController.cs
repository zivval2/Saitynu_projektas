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
    public class RegistrationController : ControllerBase
    {
        private readonly ClientContext _context;

        public RegistrationController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/Registration
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IEnumerable<Registration> GetRegistrations()
        {
            return _context.Registrations;
        }

        //[HttpGet("{id}/serviceId/{serviceId}")]
        //// [AllowAnonymous]
        //public async Task<IActionResult> GetRegistration([FromRoute] int id, [FromRoute] int serviceId)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }



        //    //var registration = "value";//_context.Registrations.Include(d=>d.Service);
        //                               //foreach(Registration d in registration)
        //                               //{
        //                               //    foreach(Service s in d.Service)
        //                               //    {
        //                               //        kazkasList.add.(d);
        //                               //    }
        //                               //}
        //                               //if (registration == null)
        //                               //{
        //                               //    return NotFound();
        //                               //}

        //    var services = await _context.Services.FindAsync(serviceId);
        //    var registration = await _context.Registrations.FindAsync(id);


        //    if (registration == null)
        //    {
        //        return NotFound();
        //    }

        //    if (services == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(services);// registration);
        //}

        // GET: api/Registration/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var idClaim = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            var registration = await _context.Registrations.FindAsync(id);
            var service = await _context.Services.FindAsync(registration.ServiceId);
            if (registration.ClientId.ToString() != idClaim.Value || service.ArtistId.ToString() != idClaim.Value)
            {
                return Unauthorized();
            }

            if (registration == null)
            {
                return NotFound();
            }

            return Ok(registration);
        }

        // PUT: api/Registration/5
        [Authorize(Roles = Role.Client)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration([FromRoute] int id, [FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var idClaim = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            if (registration.ClientId.ToString() != idClaim.Value)
            {
                return Unauthorized();
            }

            if (id != registration.RegistrationId)
            {
                return BadRequest();
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
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

        // POST: api/Registration
        [Authorize(Roles = Role.Client)]
        [HttpPost]
        public async Task<IActionResult> PostRegistration([FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var idClaim = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault();

            if (registration.ClientId.ToString() != idClaim.Value)
            {
                return Unauthorized();
            }
            var time = await _context.Times.FindAsync(registration.TimeId);
            if(time.IsUsed || !time.IsWorking)
            {
                return BadRequest();
            }

            registration.RegistrationDate = DateTime.Now;
            time.IsUsed = true;
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistration", new { id = registration.RegistrationId }, registration);
        }

        // DELETE: api/Registration/5
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return Ok(registration);
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }
    }
}