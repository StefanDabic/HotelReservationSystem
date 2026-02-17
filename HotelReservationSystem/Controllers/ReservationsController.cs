using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using System.Linq;

namespace HotelReservationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly HotelContext _context;
        public ReservationsController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Room)
                .Select(r => new
                {
                    r.ReservationId,
                    Korisnik = r.User != null ? r.User.Name : "Nepoznat",
                    Soba = r.Room != null ? r.Room.RoomNumber : "Nepoznata",
                    r.StartDate,
                    r.EndDate,
                    r.Status
                })
                .ToList();

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Room)
                .FirstOrDefault(r => r.ReservationId == id);

            if (reservation == null)
                return NotFound(new { message = $"Rezervacija sa ID={id} nije pronađena." });

            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult CreateReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (reservation.EndDate <= reservation.StartDate)
                return BadRequest(new { message = "Datum završetka mora biti posle datuma početka." });

            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == reservation.RoomId);
            if (room == null)
                return NotFound(new { message = "Soba sa zadatim ID-em ne postoji." });

            bool zauzeta = _context.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                ((reservation.StartDate >= r.StartDate && reservation.StartDate <= r.EndDate) ||
                 (reservation.EndDate >= r.StartDate && reservation.EndDate <= r.EndDate))
            );

            if (zauzeta)
                return BadRequest(new { message = "Soba je već rezervisana u izabranom periodu." });

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.ReservationId }, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == id);
            if (reservation == null)
                return NotFound(new { message = $"Rezervacija sa ID={id} ne postoji." });

            if (updatedReservation.EndDate <= updatedReservation.StartDate)
                return BadRequest(new { message = "Datum završetka mora biti posle datuma početka." });

            reservation.UserId = updatedReservation.UserId;
            reservation.RoomId = updatedReservation.RoomId;
            reservation.StartDate = updatedReservation.StartDate;
            reservation.EndDate = updatedReservation.EndDate;
            reservation.Status = updatedReservation.Status;

            _context.SaveChanges();
            return Ok(new { message = "Rezervacija uspešno ažurirana." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == id);
            if (reservation == null)
                return NotFound(new { message = $"Rezervacija sa ID={id} nije pronađena." });

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return Ok(new { message = "Rezervacija uspešno obrisana." });
        }
    }
}
