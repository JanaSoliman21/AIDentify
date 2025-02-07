using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Subscriber :User
    {
        public string SubscriptionId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SubscriptionId))]
        public Subscription Subscription { get; set; }

        public string PaymentId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(PaymentId))]
        public Payment Payment { get; set; }

        [ValidateNever]
        public List<Report> Reports { get; set; }
    }
}
