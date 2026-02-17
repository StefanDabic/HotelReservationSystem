using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using System.Linq;

namespace HotelReservationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HotelContext _context;
        public UsersController(HotelContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Include(u => u.Role)
                .Select(u => new
                {
                    u.UserId,
                    u.Name,
                    u.Email,
                    Uloga = u.Role != null ? u.Role.RoleName : "Nepoznata"
                })
                .ToList();

            return Ok(users);
        }

        // ✅ GET: api/Users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .Where(u => u.UserId == id)
                .Select(u => new
                {
                    u.UserId,
                    u.Name,
                    u.Email,
                    Uloga = u.Role != null ? u.Role.RoleName : "Nepoznata"
                })
                .FirstOrDefault();

            if (user == null)
                return NotFound(new { message = $"Korisnik sa ID={id} nije pronađen." });

            return Ok(user);
        }

        // ✅ POST: api/Users
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Provera da li već postoji korisnik sa istim emailom
            if (_context.Users.Any(u => u.Email == user.Email))
                return BadRequest(new { message = "Korisnik sa ovom email adresom već postoji." });

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // ✅ PUT: api/Users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound(new { message = $"Korisnik sa ID={id} ne postoji." });

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.RoleId = updatedUser.RoleId;

            _context.SaveChanges();
            return Ok(new { message = "Korisnik uspešno ažuriran." });
        }

        // ✅ DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound(new { message = $"Korisnik sa ID={id} nije pronađen." });

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(new { message = "Korisnik uspešno obrisan." });
        }
    }
}
