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
        public String Id { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        [ValidateNever]
        public Role Role { get; set; }

        [ValidateNever]
        public List<Model> Models { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Duration { get; set; }

        [Required]
        [Range(7,20)]
        public long Price { get; set; }

        
        [ValidateNever]
        public bool Updateable { get; set; }


    }
}
