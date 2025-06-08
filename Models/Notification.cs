using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Notification
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string NotificationContent { get; set; }

        [ValidateNever]
        public DateTime SentAt { get; set; }


        // Either a Doctor or a Student get notifications
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
