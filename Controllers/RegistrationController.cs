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
    public class RegistrationController : ControllerBase
    {
        private readonly ClientContext _context;

        public RegistrationController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/Registration
        [HttpGet]
        public IEnumerable<Registration> GetRegistrations()
        {
            return _context.Registrations;
        }

        [HttpGet("{id}/serviceId/{serviceId}")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetRegistration([FromRoute] int id, [FromRoute] int serviceId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            //var registration = "value";//_context.Registrations.Include(d=>d.Service);
                                       //foreach(Registration d in registration)
                                       //{
                                       //    foreach(Service s in d.Service)
                                       //    {
                                       //        kazkasList.add.(d);
                                       //    }
                                       //}
                                       //if (registration == null)
                                       //{
                                       //    return NotFound();
                                       //}

            var services = await _context.Services.FindAsync(serviceId);
            var registration = await _context.Registrations.FindAsync(id);


            if (registration == null)
            {
                return NotFound();
            }

            if (services == null)
            {
                return NotFound();
            }

            return Ok(services);// registration);
        }

        // GET: api/Registration/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistration([FromRoute] int id)
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

            return Ok(registration);
        }

        // PUT: api/Registration/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration([FromRoute] int id, [FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        [HttpPost]
        public async Task<IActionResult> PostRegistration([FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistration", new { id = registration.RegistrationId }, registration);
        }

        // DELETE: api/Registration/5
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