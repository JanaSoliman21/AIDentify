using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDentify.Models
{
    public class Message
    {
        [Key]
        public string MessageId { get; set; }

        public string? SenderIdD { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SenderIdD))]
        public Doctor? Sender { get; set; }

        public string? ReceiverIdD { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ReceiverIdD))]
        public Doctor? Receiver { get; set; }

        public string? SenderIdS { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(SenderIdD))]
        public Student? SenderS { get; set; }

        public string? ReceiverIdS { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(ReceiverIdS))]
        public Student? ReceiverS { get; set; }

        [Required]
        public string Context { get; set; }

        [Required]
        public DateTime SentAt {  get; set; } 
    }
}
