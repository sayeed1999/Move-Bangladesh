using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RideSharing.AuthenticationAPI.Models;
using RideSharing.Common.Constants;

namespace RideSharing.AuthenticationAPI.Controllers
{
    [ApiController]
    public class AuthController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager
    ) : ControllerBase
    {
        [HttpPost("register/external")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser 
            { 
                UserName = model.Email, 
                Email = model.Email,
                PhoneNumber = model.Phone,
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                switch (model.Role) 
                {
                    case ApplicationRole.Customer:
                    case ApplicationRole.Driver:
                        await userManager.AddToRoleAsync(user, model.Role);
                        break;
                    default:
                        return BadRequest("Role not permitted or valid");
                }
                
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }
    }
}
