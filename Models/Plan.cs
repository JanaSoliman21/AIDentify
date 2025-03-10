using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Plan
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string PlanName { get; set; }

        [Required]
        public int Duration { get; set; }   // in months

        [Required]
        public int MaxScans { get; set; }

        [Required]
        public int MaxPatients { get; set; }

        [Required]
        [Range(7,20)]
        public long Price { get; set; }

    }
}
