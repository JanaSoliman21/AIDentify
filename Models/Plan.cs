using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class Plan
    {
        [Key]
        protected String Id { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        [ValidateNever]
        protected Role Role { get; set; }

        [ValidateNever]
        protected List<Model> Models { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        protected DateTime Duration { get; set; }

        [Required]
        [Range(7,20)]
        protected long Price { get; set; }

        
        [ValidateNever]
        protected bool Updateable { get; set; }


    }
}
