using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public  abstract class Results
    {
        [Key]
        protected string Id { get; set; }

        protected string ModelId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ModelId))]
        protected Model Model { get; set; }

        protected object Result { get; set; }

    }
}
