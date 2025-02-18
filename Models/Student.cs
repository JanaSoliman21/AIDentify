using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Student: User
    {
        [ValidateNever]
        public string University { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public long TotalPOintsEarned { get; set; }
    }
}
