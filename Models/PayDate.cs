using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class PayDate
    {
        [Required]
        [DataType(DataType.DateTime)]
        protected DateTime LastDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        protected DateTime NextDate { get; set; }


    }
}
