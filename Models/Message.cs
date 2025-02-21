using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Message
    {
        [Key]
        public string MessageId { get; set; }

        public string? SenderId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SenderId))]
        public User? Sender { get; set; }

        public string? ReceiverId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ReceiverId))]
        public User? Receiver { get; set; }

        [Required]
        public string Context { get; set; }

        [Required]
        public DateTime SentAt {  get; set; } 
    }
}
