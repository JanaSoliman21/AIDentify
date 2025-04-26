using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Plan
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [ValidateNever]
        public string PlanName { get; set; } = string.Empty;

        [ValidateNever]
        public int Duration { get; set; } = -1;  // in months

        [ValidateNever]
        public int MaxScans { get; set; } = -1;

        [ValidateNever]
        public int MaxPatients { get; set; } = -1;

        [ValidateNever]
        public long Price { get; set; } = -1;

    }
}
