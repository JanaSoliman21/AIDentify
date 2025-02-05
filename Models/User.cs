using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AIDentify.Models.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace AIDentify.Models
{
    public class User
    {
        [Key]
        [Column("Id")]
        protected string UserID { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        protected string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        protected string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        protected string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        protected string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        protected string Password { get; set; }

        [Required]
        [Range (18, 100)]
        protected int Age { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        protected Gender Gender { get; set; }
    }
}
