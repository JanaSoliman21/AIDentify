using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Review
    {
        [Key]
        [Column ("id")]
        public int ReviewId { get; set; }

        [Required]
        public string ReviewItSelf { get; set; }

        public string ModelId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ModelId))]
        public Model Model { get; set; }
    }
}
