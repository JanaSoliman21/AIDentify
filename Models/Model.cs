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
        protected string ModelName { get; set; }

        [Required]
        protected object ModelItSelf { get; set; }

        [Required]
        protected string Accuracy { get; set; }

        protected string GeneralFeedback { get; set; }


        protected Review Review { get; set; }


    }
}
