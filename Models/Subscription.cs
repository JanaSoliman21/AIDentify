using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Subscription
    {
        [Key]
        protected string Id { get; set; }

        [Required]
        protected Plan Plan { get; set; }

        protected PayDate PayDate { get; set; }

        protected Subscriber Subscriber { get; set; }

        protected bool IsPaid { get; set; }

    }
}
