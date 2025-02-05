using System.ComponentModel.DataAnnotations;
using AIDentify.Models.Enums;

namespace AIDentify.Models
{
    public class Payment
    {
        [Key]
        protected string Id { get; set; }

        protected Subscriber Subscriber { get; set; }

        protected WayOfPayment WayOfPayment { get; set; }

        [Required]
        [Range (5, 100)]
        protected long Amount {  get; set; }

    }
}
