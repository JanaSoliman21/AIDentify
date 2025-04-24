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

        [Required]
        public string TheQuestion { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        [Required]
        public List<string> Options { get; set; }

        //public string? QuizId { get; set; }
        //[ValidateNever]
        //[ForeignKey(nameof(QuizId))]
        //[JsonIgnore]
        //public Quiz? Quiz { get; set; }
    }
}
