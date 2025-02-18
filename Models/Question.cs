using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Question
    {
        [Key]
        public string QuestionId { get; set; }

        [Required]
        public string TheQuestion { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        [Required]
        public List<string> Options { get; set; }
    }
}
