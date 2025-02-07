using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Model
    {
        [Key]
        [Column("Id")]
        public string ModelID { get; set; }
        [Required]
        [StringLength (50)]
        public string ModelName { get; set; }

        /*[Required]
        public object ModelItSelf { get; set; }*/

        [Required]
        [Range (0, 100)]
        public string Accuracy { get; set; }

        [StringLength (200)]
        public string GeneralFeedback { get; set; }

        [ValidateNever]
        public List<Review> Review { get; set; }


        [ValidateNever]
        public List<Result> Results { get; set; }


    }
}
