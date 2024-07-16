using Eshop.Models;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Controllers;

[Authorize(Roles ="Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task <IActionResult> Index()
    {
        return View(await _roleManager.Roles.ToListAsync());
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Add(RoleFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index), await _roleManager.Roles.ToListAsync());
        }

        
        if(await _roleManager.RoleExistsAsync(model.Name))
        {
            ModelState.AddModelError("Name", "Role is exists!");
            return View(nameof(Index), await _roleManager.Roles.ToListAsync());
        }

        await _roleManager.CreateAsync(new IdentityRole(model.Name));
        return RedirectToAction(nameof(Index));
       
    }
}
