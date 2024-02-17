using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage ="Student name is required")]
        [StringLength (30)]
        public string StudentName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        [Range(10,20)]
        public int age { get; set; }

        public string Password { get; set; }
        [Compare("Password")]
        public string confirmPasword { get; set; }
        
        public DateTime AdmissionDate { get; set; }
    }
}
