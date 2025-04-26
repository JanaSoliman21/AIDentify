using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using AIDentify.Models;

namespace AIDentify.Models
{
    public class QuizAttempt
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string? StudentId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(StudentId))]
        [JsonIgnore]
        public Student? Student { get; set; }

        public string? QuizId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(QuizId))]
        [JsonIgnore]
        public Quiz? Quiz { get; set; }

        [Required]
        public List<string> SelectedAnswers { get; set; } = new List<string>();

        [Required]
        public int PointsEarned { get; set; }
    }
}