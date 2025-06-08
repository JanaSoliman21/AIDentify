using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AIDentify.Models
{
    public class Patient
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string PatientName { get; set; }

        [ValidateNever]
        public Age age { get; set; }

        [ValidateNever]
        public Gender gender { get; set; }

        public string? DoctorId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(DoctorId))]
        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        public String DoctorName { get; set; }


        [ValidateNever]
        public ICollection<MedicalHistory> MedicalHistories { get; set; }

    }
}
