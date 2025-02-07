using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class PayDate
    {
        [Key]
        public string PayDateId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime NextDate { get; set; }


    }
}
