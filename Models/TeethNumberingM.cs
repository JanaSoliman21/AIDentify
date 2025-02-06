using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class TeethNumberingM : Results
    {
        [Key]
        protected string TeethNumberingMId { get; set; }

        protected new int Result { get; set; }
    }
}
