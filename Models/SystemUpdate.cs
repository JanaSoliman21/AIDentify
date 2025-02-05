using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
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
        protected UpdateType UpdateType { get; set; }

    }
}
