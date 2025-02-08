using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public  abstract class Result
    {
        [Key]
        public string Id { get; set; }

        public string ModelId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ModelId))]
        public Model Model { get; set; }


        public string ResultValue { get; set; }

    }
}
