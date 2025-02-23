using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Plan
    {
        [Key]
        public string PlanId { get; set; }

        [Required]
        public string PlanName { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDate { get; set; } 

        [Required]
        public int MaxScans { get; set; }

        [Required]
        public int MaxPatients { get; set; }

        [Required]
        [Range(7,20)]
        public long Price { get; set; }

    }
}
