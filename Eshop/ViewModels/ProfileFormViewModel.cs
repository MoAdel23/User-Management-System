using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Eshop.ViewModels;

public class ProfileFormViewModel
{

    public string Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]

    [Display(Name = "First Name")]
    public string FirstName { get; set; }


    [Required,]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]

    [Display(Name = "Last Name")]
    public string LastName { get; set; }


    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]


    [Display(Name = "User Name")]
    public string UserName { get; set; }


    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [ValidateNever]
    public List<RoleViewModel> Roles { get; set; }

}
