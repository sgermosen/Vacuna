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
    [Route("api/inmunization")]
    [ApiController] 
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class InmunizationsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly string container = "InmunizationCards";
        private readonly IMapper mapper;
        private readonly IStorageSaver storageSaver;

        public InmunizationsController(UserManager<ApplicationUser> userManager,
          IMapper mapper,
          IStorageSaver storageSaver,
            ApplicationDbContext context)
        {
            this.userManager = userManager; 
            this.mapper = mapper;
            this.storageSaver = storageSaver;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InmunizationDTO>>> Get()
        {
            var user = await GetConectedUser();
             
            var inmunizations = await context.Inmunizations.Where(p=>p.UserId == user.Id).ToListAsync();
            return mapper.Map<List<InmunizationDTO>>(inmunizations);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InmunizationCreationDTO model)
        {
            var user = await GetConectedUser();
            
            var inmunization = mapper.Map<Inmunization>(model);
            if (model.Photo != null)
                inmunization.CardPicture = await storageSaver.SaveFile(container, model.Photo);
            inmunization.UserId = user.Id;

            context.Add(inmunization);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<IdentityUser> GetConectedUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }
    }
}
