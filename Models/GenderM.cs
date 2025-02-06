using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class GenderM : Results
    {
        [Key]
        protected string GenderMId { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        [ValidateNever]
        protected new Gender Result { get; set; }

    }
}
