using System.ComponentModel.DataAnnotations;

namespace NaturePlanetSolutionCore.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }



        
    }
}
