using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class GenderM : Results
    {
        [JsonConverter(typeof(StringEnumConverter))]
        protected new Gender Result { get; set; }

    }
}
