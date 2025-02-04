using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AIDentify.Models.Enums;

namespace AIDentify.Models
{
    public class User
    {
        [Key]
        [Column("Id")]
        protected string UserID { get; set; }

        [Required]
        protected string UserName { get; set; }

        [Required]
        protected string FirstName { get; set; }

        [Required]
        protected string LastName { get; set; }

        [Required]
        protected string Email { get; set; }

        [Required]
        protected string Password { get; set; }

        [Required]
        protected int Age { get; set; }

        [Required]
        protected Gender Gender { get; set; }
    }
}
