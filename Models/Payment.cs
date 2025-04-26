using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [ValidateNever]
        public WayOfPayment WayOfPayment { get; set; }

        [Required]
        public long Amount { get; set; } = -1;

        [Required]
        public DateTime PaymentDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [ValidateNever]
        public PaymentStatues Status { get; set; }

        // Either a Doctor or a Student makes the payment
        public string? DoctorId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(DoctorId))]
        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        public string? StudentId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(StudentId))]
        [JsonIgnore]
        public Student? Student { get; set; }

    }
}
