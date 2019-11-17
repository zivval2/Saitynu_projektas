using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Saitynu_projektas.Models;

namespace Saitynu_projektas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string SECRET_KEY = "abcdef123564adasdasdasdasdads";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthController.SECRET_KEY));

        private readonly ClientContext _context;

        public AuthController(ClientContext context)
        {
            _context = context;
        }

       // [HttpGet("{username}/{password}")]
        [HttpPost]
        //public async Task<IActionResult> GetAuth([FromRoute]string username,[FromRoute] string password)
        public async Task<IActionResult> GetAuth([FromBody] User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int user1 = _context.Users.Where(l => l.Username == user.Username).Where(l => l.Password == user.Password).Select(l => l.UserId).FirstOrDefault();
            // int user = _context.Users.Where(l => l.Username == username).Where(l => l.Password == password).Select(l => l.UserId).FirstOrDefault();
            //var x = user.Select(l => l.UserId).FirstOrDefault();
            // var artist2 = _context.Users.Where(l => l.Password == password);
           string role = _context.Users.Where(l => l.Username == user.Username).Where(l => l.Password == user.Password).Select(l => l.Role).FirstOrDefault();
            if (user1 == 0)// || artist2==null || artist != artist2)
            {
                return NotFound();
            }
            //user.Select();
            // string role = user.(role => l.Role).ToString();


            return Ok(new JwtSecurityTokenHandler().WriteToken(GenerateToken(user.Username, role)));//"Admin")));

           // return Ok(new JwtSecurityTokenHandler().WriteToken(GenerateToken(username, "Admin")));
            //return Ok(new ObjectResult(GenerateToken(username, "Admin")));
        }

        private JwtSecurityToken GenerateToken(string username, string role)
        {
            Environment.SetEnvironmentVariable("KEY_FOR_SECRET", "kazkas123213das_ASdasdadasdasdsada");

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY_FOR_SECRET")));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // add claims
            var claims = new List<Claim>();
           // var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, role));
            claims.Add(new Claim(ClaimTypes.Name, username));
            //claims.Add(new Claim(ClaimTypes.Role, "Lecturer"));
            //claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));

            //create token
            var token = new JwtSecurityToken(
                //issuer: "http://localhost:57032/",
                //audience: "http://localhost:57032/",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: signingCredentials,
                //Id: lecturer.IdLecturer,
                claims: claims

                );
            return token;
            //var claims = new List<Claim>();
            //claims.Add(new Claim(ClaimTypes.Role, role));
            //claims.Add(new Claim(ClaimTypes.Name, username));

            //var token = new JwtSecurityToken(
            //    //claims: new Claim[]
            //    //{
            //    //    new Claim(ClaimTypes.Name, username),
            //    //    new Claim(ClaimTypes.Role, role)
            //    //},
            //    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            //    expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
            //    signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256),
            //    claims : claims
            //    );
            //return token;// new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}