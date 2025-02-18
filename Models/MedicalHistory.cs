using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class MedicalHistory
    {
        [Key]
        public string MedicalHistoryId { get; set; }

        public string PatientId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        [ValidateNever]
        public byte[] XRay { get; set; }

        [ValidateNever]
        public string Diagnosis { get; set; }

        [ValidateNever]
        public int TeethCount { get; set; }

        [ValidateNever]
        public DateTime VisitDate { get; set; }
    }
}
