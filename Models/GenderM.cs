using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class GenderM : Result
    {
        public Gender GenderValue
        {
            get => Enum.Parse<Gender>(ResultValue);
            set => ResultValue = value.ToString();
        }

    }
}
