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
        public string Id { get; set; } = string.Empty;

        [MaxLength(100)]
        public string UpdatedDescription { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [ValidateNever]
        public UpdateType UpdateType { get; set; } = UpdateType.None;

        public string? AdminId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(AdminId))]
        public Admin? Admin { get; set; }
    }
}
