using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Report
    {
        [Key]
       protected string Id { get; set; }

        [ValidateNever]
        protected AgeM AgeM { get; set; }

        [ValidateNever]
        protected GenderM GenderM { get; set; }

        [ValidateNever]
        protected DiseaseM DiseaseM { get; set; }

        [ValidateNever]
        protected TeethNumberingM TeethNumberingM { get; set; }

        [ValidateNever]
        protected Subscriber Subscriber { get; set; }

    }
}
