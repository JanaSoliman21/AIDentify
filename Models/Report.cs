using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Report
    {
        [Key]
        protected string Id { get; set; }

        protected string AgeMId { get; set; }
        
        [ValidateNever]
        [ForeignKey(nameof(AgeMId))]
        protected AgeM AgeM { get; set; }

        protected string GenderMId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(GenderMId))]
        protected GenderM GenderM { get; set; }

        protected string DiseaseMId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(DiseaseMId))]
        protected DiseaseM DiseaseM { get; set; }

        protected string TeethNumberingMId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(TeethNumberingMId))]
        protected TeethNumberingM TeethNumberingM { get; set; }

        protected string SubscriberId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SubscriberId))]
        protected Subscriber Subscriber { get; set; }

    }
}
