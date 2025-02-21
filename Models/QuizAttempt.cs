using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class QuizAttempt
    {
        [Key]
        public string QuizAttemptId { get; set; }

        public string? StudentId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        public string? QuizId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(QuizId))]
        public Quiz? Quiz { get; set; }

        [Required]
        public List<string> SelectedAnswers { get; set; }

        [Required]
        public int PointsEarned { get; set; }
    }
}
