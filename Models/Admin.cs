using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace AIDentify.Models
{
    public class Admin
    {
        [Key]
        public string AdminId { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string AdminName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string AdminEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }

        [ValidateNever]
        public List<SystemUpdate> SystemUpdates { get; set; }
    }
}
