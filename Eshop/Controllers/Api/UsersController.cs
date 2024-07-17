using Eshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers.Api;


[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController :ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    
    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        
    }

    //[HttpDelete("{userId}")]
    [HttpDelete()]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return NotFound();

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
            throw new Exception();

        return Ok();
    }
    
}
