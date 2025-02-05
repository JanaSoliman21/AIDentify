namespace AIDentify.Models
{
    public class Report
    {
       protected string Id { get; set; }
        protected AgeM AgeM { get; set; }
        protected GenderM GenderM { get; set; }
        protected DiseaseM DiseaseM { get; set; }
        protected TeethNumberingM TeethNumberingM { get; set; }

        protected Subscriber Subscriber { get; set; }

    }
}
