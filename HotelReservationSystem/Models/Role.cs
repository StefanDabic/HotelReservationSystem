using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Naziv uloge")]
        public string RoleName { get; set; } = string.Empty;

        // 🔗 Navigaciona veza ka User tabeli
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
