using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Quiz
    {
        [Key]
        public string QuizId { get; set; }

        [Required]
        public List<Question> Questions { get; set; }
    }
}
