using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }

        [Required]
        public string PatientName { get; set; }

        [ValidateNever]
        public Age age { get; set; }

        [ValidateNever]
        public Gender gender { get; set; }

        public string? DoctorId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(DoctorId))]
        public Doctor? Doctor { get; set; }

        [ValidateNever]
        public List<MedicalHistory> MedicalHistories { get; set; }

    }
}
