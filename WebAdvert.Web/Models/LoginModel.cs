﻿using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Password")]
        public bool RememberMe { get; set; }
    }
}
