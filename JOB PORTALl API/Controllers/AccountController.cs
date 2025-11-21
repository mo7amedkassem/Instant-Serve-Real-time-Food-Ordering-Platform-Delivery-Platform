using Booking.Core.Dtos;
using Booking.Core.EmailContracts;
using Job.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTALl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signIn;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _service;
        private readonly IEmailService _emailService;

        public AccountController( SignInManager<AppUser> signIn ,UserManager<AppUser> userManager  , IAuthService service , IEmailService emailService)
        {
            _signIn = signIn;
            _userManager = userManager;
            _service = service;
            _emailService = emailService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return Unauthorized();

            var result = await _signIn.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded is false) return Unauthorized();

            return Ok(new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                Token = await _service.CreateTokenAsync(user, _userManager)
            });
            
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var baseusername =  model.Email.Split('@')[0];
            int count = 1;
            var username = baseusername;
            while (await _userManager.FindByNameAsync(username) is not null)
            {
                username = $"{baseusername}{count}";
                count++;
            }
            var user = new AppUser
            {
                UserName = username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }

            return Ok(new UserDto
            {
              UserName = user.UserName,
              Email = user.Email,
                Token = await _service.CreateTokenAsync(user, _userManager)
            });


        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTo model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            await _userManager.SetAuthenticationTokenAsync(user, "PasswordReset", "Code", token);


            await _emailService.SendEmailAsync(user.Email, "Password Reset Code", $"Your password reset code: {token}");

            return Ok(new { message = "Password reset link has been sent to your email." });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("No user associated with this email");
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }
            return Ok("Password has been reset successfully.");
        }


    }
}
