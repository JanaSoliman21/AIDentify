using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class DiseaseM : Result
    {
        public Byte DiseaseValue
        {
            get => Enum.Parse<Byte>(ResultValue);
            set => ResultValue = value.ToString();
        }
    }
}
