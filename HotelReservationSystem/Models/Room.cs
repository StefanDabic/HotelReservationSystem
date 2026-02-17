using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Broj sobe")]
        [MaxLength(10)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Kapacitet (broj kreveta)")]
        [Range(1, 10, ErrorMessage = "Kapacitet mora biti između 1 i 10.")]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "Cena po noćenju (€)")]
        [Range(10, 1000, ErrorMessage = "Cena mora biti između 10 i 1000€.")]
        public decimal PricePerNight { get; set; }

        [Required]
        [Display(Name = "Status sobe")]
        [MaxLength(20)]
        public string Status { get; set; } = "Slobodna"; 

        
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
