using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class TrendingNews
    {
        [Key]
        public string NewsId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Context { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
