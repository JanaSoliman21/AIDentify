using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class AgeM : Results
    {
        [JsonConverter(typeof(StringEnumConverter))]
        protected new Age Result { get; set; }

    }
}
