namespace AIDentify.Models
{
    public class Admin: User
    {
        protected List<SystemUpdate> SystemUpdates { get; set; }
    }
}
