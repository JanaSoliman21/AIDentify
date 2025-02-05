using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Model
    {
        [Key]
        [Column ("Id")]
        protected string ModelID { get; set; }
        [Required]
        [StringLength (50)]
        protected string ModelName { get; set; }

        [Required]
        protected object ModelItSelf { get; set; }

        [Required]
        [Range (0, 100)]
        protected string Accuracy { get; set; }

        [StringLength (200)]
        protected string GeneralFeedback { get; set; }


        protected List<Review> Review { get; set; }


    }
}
