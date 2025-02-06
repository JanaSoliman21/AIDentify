using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Review
    {
        [Key]
        [Column ("id")]
        protected int ReviewId { get; set; }

        [Required]
        protected string ReviewItSelf { get; set; }

        protected string ModelId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ModelId))]
        protected Model Model { get; set; }
    }
}
