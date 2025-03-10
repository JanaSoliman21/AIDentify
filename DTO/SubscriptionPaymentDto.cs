using AIDentify.Models;

namespace AIDentify.DTO
{
    public class SubscriptionPaymentDto
    {
        public Subscription Subscription { get; set; }
        public Payment Payment { get; set; }
    }
}
