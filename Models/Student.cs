using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

namespace AIDentify.Models
{
    public class Student
    {
        [Key]
        public string Student_ID { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? SubscriptionId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SubscriptionId))]
        [JsonIgnore]
        public Subscription? Subscription { get; set; }

        [ValidateNever]
        public List<Payment>? Payments { get; set; } = new();

        [Required]
        public string University { get; set; }

        [Required]
        public int Level { get; set; }

        public long? TotalPOintsEarned { get; set; } = 0;
    }
}
