using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Doctor
    {
        [Key]
        public string Doctor_ID { get; set; }

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
        [System.Text.Json.Serialization.JsonIgnore]
        public string Password { get; set; }

        public string? SubscriptionId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SubscriptionId))]
        [JsonIgnore]
        public Subscription? Subscription { get; set; }

        [ValidateNever]
        public List<Payment>? Payments { get; set; } = new();

        [Required]
        public string ClinicName {  get; set; }

        [ValidateNever]
        public List<Patient>? patients { get; set; }
    }
}
