using Microsoft.AspNetCore.Mvc;
using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using System.Linq;

namespace HotelReservationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly HotelContext _context;

        public RolesController(HotelContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Roles
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }

        // ✅ GET: api/Roles/{id}
        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role == null)
                return NotFound(new { message = "Uloga nije pronađena." });

            return Ok(role);
        }

        // ✅ POST: api/Roles
        [HttpPost]
        public IActionResult CreateRole(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
                return BadRequest(new { message = "Naziv uloge je obavezan." });

            _context.Roles.Add(role);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, role);
        }

        // ✅ PUT: api/Roles/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, Role updatedRole)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role == null)
                return NotFound(new { message = "Uloga nije pronađena." });

            if (string.IsNullOrWhiteSpace(updatedRole.RoleName))
                return BadRequest(new { message = "Naziv uloge ne može biti prazan." });

            role.RoleName = updatedRole.RoleName;
            _context.SaveChanges();

            return NoContent();
        }

        // ✅ DELETE: api/Roles/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role == null)
                return NotFound(new { message = "Uloga nije pronađena." });

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
