using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Subscription
    {
        [Key]
        public string SubscriptionId { get; set; }

        public string? PlanId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PlanId))]
        public Plan? Plan { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; }

        public string? PayDateId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PayDateId))]
        public PayDate? PayDate { get; set; }

        [ValidateNever]
        public bool IsPaid { get; set; }

    }
}
