using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacunaAPI.DTOs;
using VacunaAPI.Entities;
using VacunaAPI.Utils;

namespace VacunaAPI.Controllers
{
    [Route("api/immunization")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImmunizationsController : PsBaseController
    {
        private readonly string container = "ImmunizationCards";

        public ImmunizationsController(UserManager<ApplicationUser> userManager, IMapper mapper, IStorageSaver storageSaver, ApplicationDbContext context) : base(userManager, mapper, storageSaver, context)
        {
        }


        //[HttpPost("validateImmunization")]
        //public async Task<ActionResult<ImmunizationDTO>> ValidateImmunization()
        //{
        //    var user = await GetConectedUser();
        //    //TODO: Validate if have the subscription or rol to make this action

        //    var user = await GetConectedUser();
        //    var inmunizations = await context.Inmunizations.Where(p => p.UserId == user.Id).ToListAsync();
        //    return mapper.Map<List<InmunizationDTO>>(inmunizations);
        //}

        [HttpGet]
        public async Task<ActionResult<List<ImmunizationDTO>>> Get()
        {
            var user = await GetConectedUser();
            var inmunizations = await Context.Immunizations.Where(p => p.UserId == user.Id).ToListAsync();
            return Mapper.Map<List<ImmunizationDTO>>(inmunizations);
        }


        [HttpGet("getByUser")]
        public async Task<ActionResult<List<ImmunizationDTO>>> GetByUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var user = await GetConectedUser();
            bool canCheckIt = false;
            canCheckIt = await UserManager.IsInRoleAsync(user, "Admin");

            if (!canCheckIt)
                return Unauthorized();

            var inmunizations = await Context.Immunizations
                .Include(p => p.Vaccine)
                .Include(p => p.Laboratory)
                .Where(p => p.UserId == userId).ToListAsync();
            return Mapper.Map<List<ImmunizationDTO>>(inmunizations);
        }

        [HttpGet("getIfUserIsImmunizedWith")]
        public async Task<ActionResult<List<ImmunizationDTO>>> GetIfUserIsImmunizedWith(string userId, [FromQuery] int[] vaccinesIds)
        {
            if (!(vaccinesIds.Length > 0))
                return BadRequest();
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var user = await GetConectedUser();
            //TODO: Change this to false
            bool canCheckIt = true;// false;
            bool isImmunized = false;
         
            //TODO: Enabled This
            //  canCheckIt = await UserManager.IsInRoleAsync(user, "Admin");

            if (!canCheckIt)
                return Unauthorized();

            var inmunizations = await Context.Immunizations
                .Include(p => p.Vaccine)
                .Include(p => p.Laboratory)
                .Where(p => p.UserId == userId).ToListAsync();

            foreach (var vaccineId in vaccinesIds)
            {
                isImmunized = inmunizations.Any(p => p.VaccineId == vaccineId);
            }

            return Ok(isImmunized);
        }

        [HttpGet("getById")]
        public async Task<ActionResult<ImmunizationDTO>> GetById(int id)
        {
            var user = await GetConectedUser();
            bool canCheckIt = false;

            var inmunization = await Context.Immunizations
                .Include(p => p.Vaccine)
                .Include(p => p.Laboratory)
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (inmunization != null)
            {
                if (inmunization.UserId != user.Id)
                {
                    canCheckIt = await UserManager.IsInRoleAsync(user, "Admin");
                    if (canCheckIt == false)
                        return Unauthorized();
                }
            }
            return Mapper.Map<ImmunizationDTO>(inmunization);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ImmunizationCreationDTO model)
        {
            var user = await GetConectedUser();

            var inmunization = Mapper.Map<Immunization>(model);
            if (model.Photo != null)
                inmunization.CardPicture = await StorageSaver.SaveFile(container, model.Photo);
            inmunization.UserId = user.Id;

            //TODO: Here we have to make validation of the center if exist or something

            Context.Add(inmunization);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<ApplicationUser> GetConectedUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await UserManager.FindByEmailAsync(email);
            return user;
        }

    }
}
