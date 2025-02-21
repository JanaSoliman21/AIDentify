using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class SystemUpdate
    {
        [Key]
        public string SystemUpdateId { get; set; }

        [MaxLength(100)]
        public string UpdatedDescribtion { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [ValidateNever]
        public UpdateType UpdateType { get; set; }

        public string? AdminId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(AdminId))]
        public Admin? Admin { get; set; }
    }
}
