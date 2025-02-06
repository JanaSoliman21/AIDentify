using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class GenderM : Results
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [ValidateNever]
        protected new Gender Result { get; set; }

    }
}
