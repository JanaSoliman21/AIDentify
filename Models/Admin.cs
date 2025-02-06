using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace AIDentify.Models
{
    public class Admin: User
    {
        [ValidateNever]
        protected List<SystemUpdate> SystemUpdates { get; set; }
    }
}
