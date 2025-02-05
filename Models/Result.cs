using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public  abstract class Results
    {
        [Key]
        protected string Id { get; set; }

        protected Model Model { get; set; }

        protected object Result { get; set; }

    }
}
