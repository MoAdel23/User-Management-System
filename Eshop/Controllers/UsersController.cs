using Eshop.Models;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Eshop.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public IActionResult Index()
    {

        var users = _userManager.Users.Select(user => new UserViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = _userManager.GetRolesAsync(user).Result
        }).ToList();
        return View(users);
    }

    public async Task<IActionResult> Add()
    {
        var roles = await _roleManager.Roles.Select(r => new RoleViewModel()
        {
            RoleId = r.Id,
            RoleName = r.Name
        }).ToListAsync();

        var viewModel = new AddUserViewModel()
        {
            Roles = roles
        };
        return View(viewModel);
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddUserViewModel model)
    {

        if (!ModelState.IsValid)
            return View(model);

        if (!model.Roles.Any(r => r.IsSelected))
        {
            ModelState.AddModelError("Roles", "Please select at least one role");
            return View(model);
        }

        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            ModelState.AddModelError("Email", "Email is already exists!");
            return View(model);
        }

        if (await _userManager.FindByNameAsync(model.UserName) != null)
        {
            ModelState.AddModelError("UserName", "User Name is already exists!");
            return View(model);
        }

        var user = new ApplicationUser()
        {
            FirstName = model.FirstName,
            Email = model.Email,
            LastName = model.LastName,
            UserName = model.UserName,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Roles", error.Description);
            }
        }
        await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));

        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);


        if (user is null)
            return NotFound();

        var roles = await _roleManager.Roles.ToListAsync();

        var viewModel = new ProfileFormViewModel()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
            Roles = roles.Select(role => new RoleViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name,
                IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProfileFormViewModel model)
    {

        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.Id);
        if (user is null)
            return NotFound();

        if (!model.Roles.Any(r => r.IsSelected))
        {
            ModelState.AddModelError("Roles", "Please select at least one role");
            return View(model);
        }

        var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
        if (userWithSameEmail != null && userWithSameEmail.Id != model.Id)
        {
            ModelState.AddModelError("Email", "Email is already assiged to another user!");
            return View(model);
        }

        var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
        if (userWithSameUserName != null && userWithSameUserName.Id != model.Id)
        {
            ModelState.AddModelError("UserName", "User Name is assiged to another user!");
            return View(model);
        }


        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.UserName = model.UserName;
        user.Email = model.Email;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Roles", error.Description);
            }
        }

        // Get roles for user
        var userRoles = await _userManager.GetRolesAsync(user);


        // Role Actions [Add , Remove]
        foreach (var role in model.Roles)
        {
            // Remove
            if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            // Add
            if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                await _userManager.AddToRoleAsync(user, role.RoleName);

        }

        return RedirectToAction(nameof(Index));
    }



    public async Task<IActionResult> ManageRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);


        if (user is null)
            return NotFound();

        var roles = await _roleManager.Roles.ToListAsync();

        var viewModel = new UserRolesViewModel()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Roles = roles.Select(role => new RoleViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name,
                IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
            }).ToList()

        };

        return View(viewModel);
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
    {
        // Get user by Id 
        var user = await _userManager.FindByIdAsync(model.UserId);


        if (user is null)
            return NotFound();

        // Get roles for user
        var userRoles = await _userManager.GetRolesAsync(user);


        // Role Actions [Add , Remove]
        foreach (var role in model.Roles)
        {
            // Remove
            if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            // Add
            if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                await _userManager.AddToRoleAsync(user, role.RoleName);

        }
        return RedirectToAction(nameof(Index));

    }



    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        // Find the user by id
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        // Delete the user
        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Json(new { success = true, message = "Item has been deleted successfully" });
        }
        else
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
    }

}
