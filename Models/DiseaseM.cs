using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class DiseaseM : Results
    {
        [ValidateNever]
        protected new Byte Result { get; set; }
    }
}
