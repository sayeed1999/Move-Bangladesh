using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RideSharing.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RideSharing.API
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;

        public UserController(
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IOptions<AppSettings> appSettings
        ) {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }

        // TODO: remove allow anonymous, and make it an admin access endpoint only!
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Response<User>>> Register(RegisterDto model)
        {
            var response = new Response<RegisterDto>();
            response.Data = model;

            if (ModelState.IsValid) // this one line with check "are all required fields of registerDto provided or not"
            {
                var user = new User
                {                    
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = String.IsNullOrEmpty(model.UserName) ? model.Email : model.UserName,
                };

                if (model.Password != model.ConfirmPassword) 
                    throw new CustomException("Password & confirm password don't match", 400);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    response.Message = "User registered successfully!";
                    User registeredUser = await _userManager.FindByEmailAsync(user.Email);

                    int addedRoleCount = 0;
                    foreach (string roleName in model.Roles)
                    {
                        string temp = roleName.Trim().ToLower();
                        if (string.IsNullOrEmpty(temp)) continue;
                        if (!(await _roleManager.RoleExistsAsync(roleName))) continue;

                        if (!(await _userManager.IsInRoleAsync(registeredUser, roleName)))
                        {
                            await _userManager.AddToRoleAsync(registeredUser, temp);
                            addedRoleCount++;
                        }
                    }
                    response.Message += " User is added to " + addedRoleCount + " respective roles.";

                }
                else
                {
                    response.Message = "Errors occured:-\n";
                    foreach (var error in result.Errors)
                    {
                        response.Message += error.Description + "\n";
                    }
                    response.Status = 400;
                }
            }
            else
            {
                response.Message = "Model State is not valid. Send proper data";
                response.Status = 400;
            }
            if (response.Status < 400) return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("roles")]
        public async Task<ActionResult<Response<RoleDto>>> CreateRole([FromBody] RoleDto newRole)
        {
            var serviceResponse = new Response<RoleDto>();
            serviceResponse.Data = newRole;
            try
            {
                //_context.Roles.Add(new IdentityRole() { Name = newRole.Name });
                //await _context.SaveChangesAsync();

                await _roleManager.CreateAsync(new IdentityRole() { Name = newRole.Name.Trim().ToLower() });
                serviceResponse.Message = "New role created!";
            }
            catch (Exception ex)
            {
                serviceResponse.Status = 400;
                serviceResponse.Message = ex.Message;
            }
            if (serviceResponse.Status < 300) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpGet("roles")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<IEnumerable<string>>>> GetAllRoles()
        {
            Response<IEnumerable<string>> serviceResponse = new Response<IEnumerable<string>>();
            var temp = new List<string>();
            try
            {
                foreach (var role in _roleManager.Roles)
                {
                    temp.Add(role.Name);
                }
                if (temp.Count == 0) serviceResponse.Message = "No roles found. Try inserting roles.";
            }
            catch (Exception ex)
            {
                serviceResponse.Status = 400;
                serviceResponse.Message = ex.Message;
            }
            serviceResponse.Data = temp;
            if (serviceResponse.Status < 300) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<Response<IEnumerable<RegisterDto>>>> GetAllUsers()
        {
            var serviceResponse = new Response<IEnumerable<RegisterDto>>();
            var users = new List<RegisterDto>();
            List<IdentityRole> dbRoles = await _roleManager.Roles.ToListAsync();

            foreach (var user in _userManager.Users)
            {
                List<string> roles = new List<string>();
                foreach (var role in dbRoles)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        roles.Add(role.Name);
                    }
                }

                users.Add(
                    new RegisterDto()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Roles = roles,
                        UserName = user.UserName
                    }
                );
            }
            serviceResponse.Data = users;
            return Ok(serviceResponse);
        }

        [HttpPut("email/{email}")]
        public async Task<ActionResult<Response<RegisterDto>>> Update(RegisterDto model, [FromRoute] string email)
        {
            var serviceResponse = new Response<RegisterDto>();
            serviceResponse.Data = model;

            if (email != model.Email)
            {
                serviceResponse.Status = 400;
                serviceResponse.Message = "Email in the route and email in the form body don't match";
                return serviceResponse;
            }

            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    serviceResponse.Status = 400;
                    serviceResponse.Message = "Model invalid. No user found";
                }
                else
                {
                    if (user.FirstName != model.FirstName) user.FirstName = model.FirstName;
                    if (user.LastName != model.LastName) user.LastName = model.LastName;
                    //if (user.UserName != model.UserName) user.UserName = model.Email;
                    if (user.Email != model.Email) user.Email = model.Email;

                    try
                    {
                        await _userManager.UpdateAsync(user);
                    }
                    catch (Exception ex)
                    {
                        serviceResponse.Status = 400;
                        serviceResponse.Message = "User data updating not successful";
                        return BadRequest(serviceResponse);
                    }

                    try
                    {
                        foreach (string roleName in model.Roles)
                        {
                            string temp = roleName.Trim().ToLower();
                            if (string.IsNullOrEmpty(temp)) continue;
                            if (!(await _roleManager.RoleExistsAsync(roleName))) continue;
                            if (!(await _userManager.IsInRoleAsync(user, roleName)))
                            {
                                await _userManager.AddToRoleAsync(user, temp);
                            }
                        }

                        var roles2 = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles2)
                        {
                            if (model.Roles.Count(x => x == role) == 0)
                            {
                                await _userManager.RemoveFromRoleAsync(user, role);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        serviceResponse.Status = 400;
                        serviceResponse.Message = "User updated without the roles for some error";
                    }
                }
            }
            else
            {
                serviceResponse.Message = "Model you provided is not valid.";
                serviceResponse.Status = 400;
            }

            if (serviceResponse.Status < 300) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<Response<RegisterDto>>> GetUserByEmail([FromRoute] string email)
        {
            var serviceResponse = new Response<RegisterDto>();

            List<IdentityRole> dbRoles = await _roleManager.Roles.ToListAsync();

            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                serviceResponse.Status = 400;
                serviceResponse.Message = "No user found.";
                return BadRequest(serviceResponse);
            }

            List<string> roles = new List<string>();
            foreach (var role in dbRoles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roles.Add(role.Name);
                }
            }

            RegisterDto ret = new RegisterDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles,
                UserName = user.UserName
            };

            serviceResponse.Data = ret;
            return Ok(serviceResponse);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(LoginDto model)
        {
            var serviceResponse = new Response<String>(); // for the token!

            User user = await _userManager.FindByEmailAsync(model.Email);
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !isValidPassword)
            {
                serviceResponse.Status = 400;
                serviceResponse.Message = "The email or pasword is incorrect!";
                return BadRequest(serviceResponse);
            }

            // Email & Password correct!
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1), //DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecretKey)), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);

            serviceResponse.Data = token;
            serviceResponse.Message = "token generated successfully!";
            return Ok(serviceResponse);
        }
    }

}
