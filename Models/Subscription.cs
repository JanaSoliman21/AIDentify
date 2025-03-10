using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Subscription
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string? PlanId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PlanId))]
        public Plan? Plan { get; set; }

        [Required]
        [ValidateNever]
        public DateTime StartDate { get; set; }

        [Required]
        [ValidateNever]
        public DateTime EndDate { get; set; }

        [Required]
        [ValidateNever]
        public DateTime WarningDate { get; set; }

        [ValidateNever]
        public bool IsPaid { get; set; } = false;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [ValidateNever]
        public SubscriptionStatus Status { get; set; }

        // Either a Doctor or a Student makes the payment
        public string? DoctorId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(DoctorId))]
        [Newtonsoft.Json.JsonIgnore]
        public Doctor? Doctor { get; set; }

        public string? StudentId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(StudentId))]
        [Newtonsoft.Json.JsonIgnore]
        public Student? Student { get; set; }

    }
}
