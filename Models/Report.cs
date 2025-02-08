using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Report
    {
        [Key]
        public string Id { get; set; }


        [Required]
        public string ResultId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ResultId))]
        public Result Result { get; set; }

        [Required]
        public string SubscriberId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SubscriberId))]
        public Subscriber Subscriber { get; set; }

    }
}
