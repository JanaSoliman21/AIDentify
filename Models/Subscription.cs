using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Subscription
    {
        [Key]
        protected string Id { get; set; }

        
        [ValidateNever]
        protected Plan Plan { get; set; }

        [ValidateNever]
        protected PayDate PayDate { get; set; }

        [ValidateNever]
        protected Subscriber Subscriber { get; set; }

        [ValidateNever]
        protected bool IsPaid { get; set; }

    }
}
