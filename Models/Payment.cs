using System.ComponentModel.DataAnnotations;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }

        [ValidateNever]
        public WayOfPayment WayOfPayment { get; set; }

        [Required]
        [Range (5, 100)]
        public long Amount {  get; set; }

    }
}
