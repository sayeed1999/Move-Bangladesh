using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RideSharing.AuthenticationAPI.Models;
using RideSharing.AuthenticationAPI.Services;
using RideSharing.Common.Constants;

// TODO:- do r&d on how to properly wrap the operations in a transaction

namespace RideSharing.AuthenticationAPI.Controllers
{
	[ApiController]
	public class AuthController(
		SignInManager<IdentityUser> signInManager,
		UserManager<IdentityUser> userManager,
		RoleManager<IdentityRole> roleManager,
		TokenService tokenService
	) : ControllerBase
	{
		[AllowAnonymous]
		[HttpPost("register/external")]
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

		[AllowAnonymous]
		[HttpPost("login/jwt")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var result = await signInManager.PasswordSignInAsync(
				model.Email,
				model.Password,
				false,
				lockoutOnFailure: false);

			if (!result.Succeeded)
			{
				return Unauthorized();
			}

			var user = await signInManager.UserManager.FindByNameAsync(model.Email);

			if (user == null)
			{
				return Unauthorized();
			}

			var tokenReponse = await tokenService.GenerateJwtTokenAsync(user);

			return Ok(tokenReponse);
		}
	}
}
