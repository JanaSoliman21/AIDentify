using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class TeethNumberingM : Result
    {
        public int TeethNumberingValue
        {
            get => Enum.Parse<int>(ResultValue);
            set => ResultValue = value.ToString();
        }
    }
}
