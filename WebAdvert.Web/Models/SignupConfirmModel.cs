using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models
{
    public class SignupConfirmModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(4, ErrorMessage = "Code is 4 digit long")]
        [Display(Name = "Code")]
        public string Code { get; set; }
    }
}
