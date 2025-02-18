using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class Plan
    {
        [Key]
        public string PlanId { get; set; }

        [Required]
        public string PlanName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Duration { get; set; }

        [Required]
        public int MaxScans { get; set; }

        [Required]
        public int MaxPatients { get; set; }

        [Required]
        [Range(7,20)]
        public long Price { get; set; }

    }
}
