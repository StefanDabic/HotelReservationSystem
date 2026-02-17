using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        [Required]
        [Display(Name = "Datum početka")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Datum završetka")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        [MaxLength(20)]
        [Display(Name = "Status rezervacije")]
        public string Status { get; set; } = "Na čekanju"; 
    }
}
