using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models
{
    public class SignupModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(6, ErrorMessage = "Password must be atleast 6 characters long")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm does not match")]
        [Display(Name = "Password")]
        public string ConfirmPassword { get; set; }
    }
}
