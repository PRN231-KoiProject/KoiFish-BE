using KoiFish_API.Services;
using KoiFish_Core.Domain.Identity;
using KoiFish_Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KoiFish_Data;
using KoiFish_Core.Services;
using KoiFish_Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using System.Data;
using System.Net;
using KoiFish_Core.Models.Auth;
using KoiFish_Core.SeedWorks.Contranst;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens.Jwt;

namespace KoiFish_API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ResultModel _resultModel;
        private readonly KoiFishDbContext _context;
        private readonly IUserService _userService;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<AppRole> roleManager, ITokenService tokenService, KoiFishDbContext context, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _resultModel = new ResultModel();
            _tokenService = tokenService;
            _context = context;
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel>> Register([FromBody] RegisterRequests request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            var user = new AppUser
            {
                FullName = request.FullName,
                Email = request.Email,
                Status = true,
                BirthYear = request.BirthYear,
                Gender = request.Gender,
                UserName = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Customer);
                _resultModel.Status = (int)HttpStatusCode.OK;
                _resultModel.Message = "Registration successful.";
                _resultModel.Success = true;

                return _resultModel;
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel>> Login([FromBody] LoginRequests request)
        {
            AppUser user = null;
            if (IsEmail(request.Email))
            {
                user = await _userManager.FindByEmailAsync(request.Email);
            }            
            if (user == null || !user.Status || user.LockoutEnabled)
            {
                _resultModel.Status = (int)HttpStatusCode.InternalServerError;
                _resultModel.Message = "Invalid email.";
                _resultModel.Success = false;
                return _resultModel;
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
            if (!result.Succeeded)
            {
                _resultModel.Status = (int)HttpStatusCode.Unauthorized;
                _resultModel.Message = "Password is incorrect. Please try again.";
                _resultModel.Success = false;
                return _resultModel;
            }
            // authorization
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(UserClaims.UserId, user.Id.ToString()),
                 new Claim(UserClaims.FullName, user.FullName),
                 new Claim(UserClaims.Role, string.Join(";", roles)),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);
            await _userManager.UpdateAsync(user);

            _resultModel.Success = true;
            _resultModel.Data = new AuthenticatedResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiredAt = user.RefreshTokenExpiryTime
            };
            _resultModel.Status = (int)HttpStatusCode.OK;
            _resultModel.Message = "Login successful.";
            return _resultModel;
        }
        private bool IsEmail(string input)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(input, emailPattern);
        }
    }
}
