using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class DiseaseM : Results
    {
        [Key]
        protected string DiseaseMId { get; set; }

        [ValidateNever]
        protected new Byte Result { get; set; }
    }
}
