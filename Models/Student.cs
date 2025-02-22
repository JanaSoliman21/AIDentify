using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Subscription? Subscription { get; set; }

        public string? PaymentId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PaymentId))]
        public Payment? Payment { get; set; }

        [Required]
        public string University { get; set; }

        [Required]
        public int Level { get; set; }

        public long? TotalPOintsEarned { get; set; }
    }
}
