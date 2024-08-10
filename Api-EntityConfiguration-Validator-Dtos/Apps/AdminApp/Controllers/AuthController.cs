using Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.UserDto;
using Api_EntityConfiguration_Validator_Dtos.Models;
using Api_EntityConfiguration_Validator_Dtos.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IMapper mapper,
        IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var existUser = await _userManager.FindByNameAsync(userRegisterDto.Username);
            if (existUser != null) return Conflict();
            AppUser appUser = new()
            {
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Fullname = userRegisterDto.Fullname,
            };
            var result = await _userManager.CreateAsync(appUser, userRegisterDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(appUser, "admin");
            return StatusCode(201);
        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            if (await _roleManager.RoleExistsAsync("member")) return BadRequest();
            await _roleManager.CreateAsync(new IdentityRole() { Name = "member" });

            if (await _roleManager.RoleExistsAsync("admin")) return BadRequest();
            await _roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
            return StatusCode(201);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user == null) return NotFound();
            var result = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            if (!result) return BadRequest();

            //JWT

            var handler = new JwtSecurityTokenHandler();

            var privateKey = Encoding.UTF8
                .GetBytes(_jwtSettings.SecretKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);

            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("id", user.Id));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Fullname));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            var roles = await _userManager.GetRolesAsync(user);
            ci.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = ci,
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                NotBefore = DateTime.UtcNow,
            };

            var token = handler.CreateToken(tokenDescriptor);

            return Ok(new { token = handler.WriteToken(token) });
        }

        [HttpGet("Profile")]
        //[Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserGetDto>(user));
        }
    }
}
