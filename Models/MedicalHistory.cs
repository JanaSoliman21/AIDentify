using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AIDentify.Models
{
    public class MedicalHistory
    {
        [Key]
        public string Id { get; set; }

        public string? PatientId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }

        public string? XRayScanId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(XRayScanId))]
        [JsonIgnore]
        public XRayScan? XRayScan { get; set; }


        [ValidateNever]
        public byte[]? DiseasePrediction { get; set; }

        [ValidateNever]
        public string Diagnosis { get; set; }

        [ValidateNever]
        public String? TeethPrediction { get; set; }

        [ValidateNever]
        public DateTime VisitDate { get; set; } = DateTime.Now;
    }
}
