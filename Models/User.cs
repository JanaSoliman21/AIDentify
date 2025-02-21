using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AIDentify.Models.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class User
    {
        [Key]
        public string UserID { get; set; }

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

    }
}
