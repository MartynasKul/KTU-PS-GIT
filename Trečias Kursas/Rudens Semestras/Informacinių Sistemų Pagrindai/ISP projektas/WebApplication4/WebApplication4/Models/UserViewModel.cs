using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class UserViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string SurName { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Current Phone Number")]
    [Required(ErrorMessage = "Current Phone Number is required.")]
    public string CurrentPhoneNumber { get; set; }

    [Display(Name = "New Phone Number")]
    [Required(ErrorMessage = "New Phone Number is required.")]
    public string NewPhoneNumber { get; set; }

    [Display(Name = "Confirm New Phone Number")]
    [Required(ErrorMessage = "Confirm New Phone Number is required.")]
    [Compare("NewPhoneNumber", ErrorMessage = "The new phone numbers do not match.")]
    public string ConfirmNewPhoneNumber { get; set; }

    


    
}
}