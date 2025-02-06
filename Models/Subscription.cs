using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Subscription
    {
        [Key]
        protected string Id { get; set; }

        protected string PlanId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PlanId))]
        protected Plan Plan { get; set; }

        protected string PayDateId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PayDateId))]
        protected PayDate PayDate { get; set; }

        [ValidateNever]
        protected bool IsPaid { get; set; }

    }
}
