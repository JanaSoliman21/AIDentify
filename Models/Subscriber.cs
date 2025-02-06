using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Subscriber :User
    {
        protected string SubscriptionId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SubscriptionId))]
        protected Subscription Subscription { get; set; }

        protected string PaymentId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PaymentId))]
        protected Payment Payment { get; set; }

        [ValidateNever]
        protected List<Report> Reports { get; set; }
    }
}
