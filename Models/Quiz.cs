using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIDentify.Models
{
    public class Quiz
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        [JsonIgnore]
        public List<Question> Questions { get; set; } = new List<Question>();

        [ValidateNever]
        [JsonIgnore]
        public List<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
    }
}
