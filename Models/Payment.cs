using System.ComponentModel.DataAnnotations;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Payment
    {
        [Key]
        protected string Id { get; set; }

        [ValidateNever]
        protected WayOfPayment WayOfPayment { get; set; }

        [Required]
        [Range (5, 100)]
        protected long Amount {  get; set; }

    }
}
