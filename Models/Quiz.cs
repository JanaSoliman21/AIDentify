using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Quiz
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        public List<Question> Questions { get; set; }

        [ValidateNever]
        public List<QuizAttempt> QuizAttempts { get; set; }
    }
}
