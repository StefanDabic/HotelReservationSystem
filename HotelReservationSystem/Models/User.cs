using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Ime i prezime")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Display(Name = "Email adresa")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; } = string.Empty;

        // 🔗 Veza sa Role tabelom (svaki korisnik ima tačno jednu ulogu)
        [Required]
        [ForeignKey(nameof(Role))]
        [Display(Name = "Uloga")]
        public int RoleId { get; set; }

        public Role? Role { get; set; }

        // 🔗 Veza sa Reservation tabelom (korisnik može imati više rezervacija)
        public ICollection<Reservation>? Reservations { get; set; } = new List<Reservation>();
    }
}
