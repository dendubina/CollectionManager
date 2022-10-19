using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CollectionsManager.BLL.DTO.User;
using CollectionsManager.BLL.Services.AuthService.Options;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CollectionsManager.BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<UserProfile> SignInAsync(SignInModel userData)
        {
            var user = await _userManager.FindByNameAsync(userData.UserName);

            if (user is null)
            {
                throw new InvalidOperationException("User not found");
            }

            var result = await _signInManager.PasswordSignInAsync(user, userData.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Invalid user name or password");
            }

            return await CreateProfile(user);
        }

        public async Task<UserProfile> SignUpAsync(SignUpModel userData)
        {
            var userToCreate = new User
            {
                UserName = userData.UserName,
                Email = userData.Email,
            };

            var result = await _userManager.CreateAsync(userToCreate, userData.Password);

            if (!result.Succeeded)
            {
                var message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description));

                throw new InvalidOperationException(message);
            }

            var createdUser = await _userManager.FindByNameAsync(userData.UserName);
            await AddDefaultRoles(createdUser);
            await AddDefaultClaims(createdUser);
             
            return await CreateProfile(createdUser);
        }

        public async Task Logout()
            => await _signInManager.SignOutAsync();

        private async Task AddDefaultClaims(User user)
        {
            var claims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            await _userManager.AddClaimsAsync(user, claims);
        }

        private async Task AddDefaultRoles(User user)
        {
            var roles = new []
            {
                "user",
            };

            await _userManager.AddToRolesAsync(user, roles);
        }

        private async Task<JwtSecurityToken> GenerateJwtToken(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET", EnvironmentVariableTarget.Machine)));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.Add(_jwtOptions.TokenExpirationTime);

            return new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );
        }

        private async Task<UserProfile> CreateProfile(User user) => new UserProfile
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            JwtToken = new JwtSecurityTokenHandler().WriteToken(await GenerateJwtToken(user))
        };

    }
}
