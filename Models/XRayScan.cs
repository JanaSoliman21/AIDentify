using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class XRayScan
    {
        [Key]
        public string Id { get; set; }

        public string? DoctorId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(DoctorId))]
        public Doctor? Doctor { get; set; }

        public string? StudentId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [ValidateNever]
        public byte[]? DiseasePrediction { get; set; }

       
        [ValidateNever]
        public String? TeethPrediction { get; set; }

        [ValidateNever]
        public Age? PredictedAgeGroup { get; set; }

        [ValidateNever]
        public Gender? PredictedGenderGroup { get; set; }

        [Required]
        public DateTime ScanDate { get; set; }

        [ValidateNever]
        public ICollection<MedicalHistory>? MedicalHistories { get; set; }
    }
}
