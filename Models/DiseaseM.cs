using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class DiseaseM : Result
    {
        [ValidateNever]
        public Byte Result { get; set; }
    }
}
