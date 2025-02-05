using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AIDentify.Models.Enums;
using Newtonsoft.Json.Converters;

namespace AIDentify.Models
{
    public class Plan
    {
        [Key]
        protected int Id { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        protected Role Role { get; set; }

        protected List<Model> Models { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        protected DateTime Duration { get; set; }

        [Required]
        [Range(7,20)]
        protected long Price { get; set; }

        [Required]
        protected bool Updateable { get; set; }


    }
}
