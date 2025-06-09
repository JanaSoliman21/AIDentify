using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AIDentify.Models
{
    public class Notification
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string NotificationContent { get; set; } = string.Empty;

        [ValidateNever]
        public DateTime SentAt { get; set; }

        [ValidateNever]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotificationStatus Status { get; set; } = NotificationStatus.sent;

        // Either a Doctor or a Student get notifications
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
