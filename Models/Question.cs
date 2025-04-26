using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [ValidateNever]
        public string TheQuestion { get; set; } = string.Empty;

        [ValidateNever]
        public string CorrectAnswer { get; set; } = string.Empty;

        [ValidateNever]
        public List<string> Options { get; set; } = new List<string>();

        public string? QuizId { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(QuizId))]
        [JsonIgnore]
        public Quiz? Quiz { get; set; }
    }
}
