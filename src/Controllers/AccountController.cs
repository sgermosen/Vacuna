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

        public AccountController(UserManager<ApplicationUser> userManager,
      IMapper mapper, ApplicationDbContext context, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
            this.configuration = configuration;
            this.signInManager = signInManager;
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



    }
}
