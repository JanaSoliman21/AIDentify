using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public  abstract class Results
    {
        [Key]
        protected string Id { get; set; }

        [ValidateNever]
        protected Model Model { get; set; }

        protected object Result { get; set; }

    }
}
