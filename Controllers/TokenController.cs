using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Saitynu_projektas.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Saitynu_projektas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private const string SECRET_KEY = "abcdef";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.SECRET_KEY));

        private readonly ClientContext _context;

        public TokenController(ClientContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Users.FindAsync(user.UserId);

            if (artist == null)
            {
                return NotFound();
            }

            return new ObjectResult(GenerateToken(user.Username));
        }

        private string GenerateToken(string username)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
