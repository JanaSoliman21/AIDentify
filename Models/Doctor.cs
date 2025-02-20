using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AIDentify.Models
{
    public class Doctor: User
    {
        [Required]
        public string ClinicName {  get; set; }

        [ValidateNever]
        public List<Patient>? patients { get; set; }
    }
}
