using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RideSharing.Common.Entities;
using RideSharing.Common.MessageBroker.Messages;
using RideSharing.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.API
{
    [Authorize(Policy = RideSharing.Entity.Constants.AuthorizationPolicy.AdminOnly)]
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private MassTransit.IPublishEndpoint _publishEndpoint;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppSettings _appSettings;

        public UserController(
            IMapper mapper,
            MassTransit.IPublishEndpoint publishEndpoint,
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            IOptions<AppSettings> appSettings
        ) {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register/internal")]
        public async Task<ActionResult<Response<RegisterDto>>> RegisterInternal(RegisterDto model)
        {
            if (!ModelState.IsValid) // this one line with check "are all required fields of registerDto provided or not"
                throw new CustomException("Model is not valid!", 400);

            return await RegisterUser(model);
        }

        [AllowAnonymous]
        [HttpPost("register/external")]
        public async Task<ActionResult<Response<RegisterDto>>> RegisterExternal(RegisterDto model)
        {
            if (!ModelState.IsValid) // this one line with check "are all required fields of registerDto provided or not"
                throw new CustomException("Model is not valid!", 400);

            if (model.Roles.Contains(RideSharing.Entity.Constants.Role.Admin)
                || model.Roles.Contains(RideSharing.Entity.Constants.Role.Moderator))
            {
                throw new CustomException("Cannot register with internal access or admininstrator roles", 401);
            }

            return await RegisterUser(model);
        }

        private async Task<ActionResult<Response<RegisterDto>>> RegisterUser(RegisterDto model)
        {
            var response = new Response<RegisterDto>();

            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = string.IsNullOrEmpty(model.UserName) ? model.Email : model.UserName,
            };

            if (model.Password != model.ConfirmPassword)
                throw new CustomException("Password & confirm password don't match", 400);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                response.Message = "User registered successfully!";
                User registeredUser = await _userManager.FindByEmailAsync(user.Email);

                int addedRoleCount = await AddRolesToUser(registeredUser, model.Roles);
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

            if (response.Status >= 400)
                throw new CustomException(response.Message, response.Status);

            // send to message broker
            await _publishEndpoint.Publish<UserRegistered>(user);

            response.Data = _mapper.Map<RegisterDto>(user);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var serviceResponse = new Response<string>(); // for the token!

            User user = await _userManager.FindByEmailAsync(model.Email);
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !isValidPassword)
                throw new CustomException("Email or password is invalid!", 400);

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),  
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim("role", userRole));
            }

            // preraring return token
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT.Secret.ToString()));
            var token = new JwtSecurityToken(
                audience: _appSettings.JWT.ValidAudience,
                issuer: _appSettings.JWT.ValidIssuer,
                expires: DateTime.Now.AddDays(1), 
                claims: authClaims, 
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            serviceResponse.Data = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(serviceResponse);
        }

        // TODO:- implement forgot password endpoint

        // TODO:- implement change password endpoint

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<RegisterDto>>>> GetAllUsers()
        {
            var serviceResponse = new Response<IEnumerable<RegisterDto>>();
            var users = new List<RegisterDto>();
            List<Role> dbRoles = await _roleManager.Roles.ToListAsync();

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

                users.Add(_mapper.Map<RegisterDto>(user));
                users[users.Count - 1].Roles = roles;
            }

            serviceResponse.Data = users;
            return Ok(serviceResponse);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<Response<RegisterDto>>> GetUserByEmail([FromRoute] string email)
        {
            var serviceResponse = new Response<RegisterDto>();

            List<Role> dbRoles = await _roleManager.Roles.ToListAsync();

            User user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new CustomException("No user found!", 404);

            List<string> roles = new List<string>();
            foreach (var role in dbRoles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roles.Add(role.Name);
                }
            }

            RegisterDto ret = _mapper.Map<RegisterDto>(user);
            ret.Roles = roles;

            serviceResponse.Data = ret;
            return Ok(serviceResponse);
        }

        [HttpPut("email/{email}")]
        public async Task<ActionResult<Response<RegisterDto>>> Update(RegisterDto model, [FromRoute] string email)
        {
            var serviceResponse = new Response<RegisterDto>();
            serviceResponse.Data = model;

            User user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new CustomException("User not found!", 404);

            // updating specific properties

            if (model.FirstName is not null && user.FirstName != model.FirstName) user.FirstName = model.FirstName;
            if (model.LastName is not null && user.LastName != model.LastName) user.LastName = model.LastName;

            await _userManager.UpdateAsync(user);
            await UpdateUserRoles(user, model.Roles);

            return Ok(serviceResponse);
        }

        [HttpDelete("email/{email}")]
        public async Task<ActionResult<Response<RegisterDto>>> Delete([FromRoute]string email)
        {
            var response = new Response<RegisterDto>();

            User user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                response.Status = 404;
                response.Message = "User not found by this email!";
            }
            else
            {
                int removedRolesCount = await RemoveRolesFromUser(user, new List<string>());
                var deleted = await _userManager.DeleteAsync(user);
                if (deleted.Succeeded == false)
                {
                    response.Message = "Deleting user failed.";
                    response.Status = 400;
                }
            }

            if (response.Status >= 400)
                throw new CustomException(response.Message, response.Status);

            return Ok(response);
        }

        private async Task UpdateUserRoles(User user, IEnumerable<string> roles)
        {
            await AddRolesToUser(user, roles);
            await RemoveRolesFromUser(user, roles);
        }

        private async Task<int> AddRolesToUser(User user, IEnumerable<string> roles)
        {
            int addedRoleCount = 0;
            var rolesInDB = await _userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                // A non-admin user can not add an admin role to his account
                if (
                    !rolesInDB.Contains(RideSharing.Entity.Constants.Role.Admin)
                    && !rolesInDB.Contains(RideSharing.Entity.Constants.Role.Moderator)
                    && (
                        role == RideSharing.Entity.Constants.Role.Admin
                        || role == RideSharing.Entity.Constants.Role.Moderator
                    )
                ) continue;

                    if (string.IsNullOrWhiteSpace(role)) continue;
                if (!(await _roleManager.RoleExistsAsync(role))) continue;
                if (!(await _userManager.IsInRoleAsync(user, role.ToLower().Trim())))
                {
                    await _userManager.AddToRoleAsync(user, role.ToLower().Trim());
                    addedRoleCount++;
                }
            }
            return addedRoleCount;
        }

        private async Task<int> RemoveRolesFromUser(User user, IEnumerable<string> newRoles)
        {
            int removedRoleCount = 0;
            var oldRoles = await _userManager.GetRolesAsync(user);

            // Only internal users can remove role
            if (
                !oldRoles.Contains(RideSharing.Entity.Constants.Role.Admin)
                && !oldRoles.Contains(RideSharing.Entity.Constants.Role.Moderator)
            ) return removedRoleCount;

            foreach (var role in oldRoles)
            {
                if (newRoles.Count(x => x == role) == 0)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                    ++removedRoleCount;
                }
            }
            return removedRoleCount;
        }
    }

}
