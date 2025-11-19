using System.ComponentModel.DataAnnotations;

namespace NaturePlanetSolutionCore.Models.ViewModels
{
    public class OrderViewModel
    {

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        
       


        
    }
}