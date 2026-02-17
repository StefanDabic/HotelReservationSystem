using Microsoft.AspNetCore.Mvc;
using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelReservationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly HotelContext _context;
        public RoomsController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _context.Rooms
                .Include(r => r.Reservations)
                .ToList();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            var room = _context.Rooms
                .Include(r => r.Reservations)
                .FirstOrDefault(r => r.RoomId == id);

            if (room == null)
                return NotFound(new { message = $"Soba sa ID={id} nije pronađena." });

            return Ok(room);
        }

        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoomById), new { id = room.RoomId }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
                return NotFound(new { message = $"Soba sa ID={id} ne postoji." });

            room.RoomNumber = updatedRoom.RoomNumber;
            room.Capacity = updatedRoom.Capacity;
            room.PricePerNight = updatedRoom.PricePerNight;
            room.Status = updatedRoom.Status;

            _context.SaveChanges();
            return Ok(new { message = "Podaci o sobi su uspešno ažurirani." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
                return NotFound(new { message = $"Soba sa ID={id} nije pronađena." });

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return Ok(new { message = "Soba je uspešno obrisana." });
        }
    }
}
