using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class AgeM : Result
    {
        public Age AgeValue
        {
            get => Enum.Parse<Age>(ResultValue);
            set => ResultValue = value.ToString();
        }


    }
}
