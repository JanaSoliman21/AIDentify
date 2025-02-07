using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class GenderM : Result
    {

        [JsonConverter(typeof(StringEnumConverter))]
        [ValidateNever]
        public Gender Result { get; set; }

    }
}
