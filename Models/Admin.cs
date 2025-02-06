using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AIDentify.Models
{
    public class Admin: User
    {
        [ValidateNever]
        protected List<SystemUpdate> SystemUpdates { get; set; }
    }
}
