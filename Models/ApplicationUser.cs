using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AIDentify.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string? University { get; set; }


        public int? Level { get; set; }

        public string? ClinicName { get; set; }

    }
}
