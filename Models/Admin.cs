using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace AIDentify.Models
{
    public class Admin : User
    {
        [ValidateNever]
        public List<SystemUpdate> SystemUpdates { get; set; }
    }
}
