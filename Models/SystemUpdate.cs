using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class SystemUpdate
    {
        [Key]
        protected string Id { get; set; }

        [MaxLength(100)]
        protected string UpdatedDescribtion { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [ValidateNever]
        protected UpdateType UpdateType { get; set; }

    }
}
