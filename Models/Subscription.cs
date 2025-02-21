using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string? PayDateId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PayDateId))]
        public PayDate? PayDate { get; set; }

        [ValidateNever]
        public bool IsPaid { get; set; }

    }
}
