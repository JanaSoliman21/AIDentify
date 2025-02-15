using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [ValidateNever]
        
        public WayOfPayment WayOfPayment { get; set; }

        [Required]
        [Range (5, 100)]
        public long Amount {  get; set; }

    }
}
