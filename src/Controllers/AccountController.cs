using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VacunaAPI.DTOs;
using VacunaAPI.Entities;
using VacunaAPI.Models;
using VacunaAPI.Services;
using VacunaAPI.Utils;

namespace VacunaAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMailHelper mailHelper;
        private readonly IAccountService _userService;

        public AccountController(UserManager<ApplicationUser> userManager,
      IMapper mapper, ApplicationDbContext context, IConfiguration configuration,
      SignInManager<ApplicationUser> signInManager, IMailHelper mailHelper, IAccountService userService)
        {
            _userService = userService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mailHelper = mailHelper;
        }

        // /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        // /api/auth/login
        [HttpPost("loginAsync")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                     mailHelper.SendMail(model.Email, "New login", "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        // /api/auth/confirmemail?userid&token
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
                 return Redirect($"{configuration["AppUrl"]}/ConfirmEmail.html");
             
            return BadRequest(result);
        }

        // api/auth/forgetpassword
        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400

            //    var user = await context.Users.Where(p => p.Email == model.Email).FirstOrDefaultAsync();
            //    if (user == null)
            //        return BadRequest("El correo ingresado no corresponde a ningún usuario.");

            //    string myToken = await userManager.GeneratePasswordResetTokenAsync(user);
            //    string link = Url.Action(
            //        "ResetPassword",
            //        "Account",
            //        new { token = myToken }, protocol: HttpContext.Request.Scheme);

            //    mailHelper.SendMail(model.Email, "Inoculapp - Reseteo de contraseña", $"<h1>Inoculapp- Reseteo de contraseña</h1>" +
            //        $"Para establecer una nueva contraseña haga clic en el siguiente enlace:</br></br>" +
            //        $"<a href = \"{link}\">Cambio de Contraseña</a>");

            //    return Ok("Las instrucciones para el cambio de contraseña han sido enviadas a su email.");

        }

        // api/auth/resetpassword
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
         //   var user = await GetConectedUser();
            //    if (user != null)
            //    {
            //        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            //        if (result.Succeeded)
            //        {
            //            return Ok("Contaseña cambiada.");
            //        }
            //        return BadRequest("Error cambiando la contraseña.");
            //    }

            //    return BadRequest("Usuario no encontrado.");

        }
 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("changePassword")]

        public async Task<ActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var user = await GetConectedUser();
            if (user != null)
            {
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Errors.FirstOrDefault().Description);
            }
            return BadRequest("Usuario no encontrado.");
        } 
       
        [HttpPost("createFromMinistry")]
        public async Task<ActionResult<AuthenticationResponse>> CreateFromMinistry([FromBody] UserCreationFromMinistryDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                BornDate = model.BornDate,
                Identification = model.Identification,
                Nationality = model.Nationality,
                IsFromMinistry = true,
                VacunationCenterId = model.VacunationCenterId
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return await CreateToken(model);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create([FromBody] UserCreationDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                BornDate = model.BornDate,
                Identification = model.Identification,
                Nationality = model.Nationality
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return await CreateToken(model);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials credentials)
        {
            var result = await signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return await CreateToken(credentials);
            }
            else
            {
                return BadRequest("Login Fail!!!");
            }
        }

        private async Task<AuthenticationResponse> CreateToken(UserCredentials credentials)
        {
            var claims = new List<Claim>() {
            new Claim("email",credentials.Email),
            new Claim("username",credentials.Email),

            };
            var user = await userManager.FindByEmailAsync(credentials.Email);
            var claimsDb = await userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDb);
            SigningCredentials creds;

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));

                creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            }
            catch (Exception ex)
            {
                throw;
            }

            var expiration = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new AuthenticationResponse()
            {
                UserId = user.Id,
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        [HttpGet("userList")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<List<UserDTO>>> UserList([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Users.AsQueryable();
            await HttpContext.InsertPaginationInHeader(queryable);
            var users = await queryable.OrderBy(p => p.Email).Paginate(pagination).ToListAsync();
            return mapper.Map<List<UserDTO>>(users);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        [HttpPost("makeAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.AddClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("removeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.RemoveClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

        private async Task<ApplicationUser> GetConectedUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }
    }
}
