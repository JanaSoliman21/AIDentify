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

        public string AgeMId { get; set; }
        
        [ValidateNever]
        [ForeignKey(nameof(AgeMId))]
        public AgeM AgeM { get; set; }

        public string GenderMId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(GenderMId))]
        public GenderM GenderM { get; set; }

        public string DiseaseMId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(DiseaseMId))]
        public DiseaseM DiseaseM { get; set; }

        public string TeethNumberingMId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(TeethNumberingMId))]
        public TeethNumberingM TeethNumberingM { get; set; }

        public string SubscriberId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SubscriberId))]
        public Subscriber Subscriber { get; set; }

    }
}
