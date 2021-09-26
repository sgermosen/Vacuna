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
    public class InmunizationsController : PsBaseController
    {
           private readonly string container = "InmunizationCards";

        public InmunizationsController(UserManager<ApplicationUser> userManager, IMapper mapper, IStorageSaver storageSaver, ApplicationDbContext context) : base(userManager, mapper, storageSaver, context)
        {
        }


        //[HttpPost("validateInmunization")]
        //public async Task<ActionResult<InmunizationDTO>> ValidateInmunization()
        //{
        //    var user = await GetConectedUser();
        //    //TODO: Validate if have the subscription or rol to make this action

        //    var user = await GetConectedUser();
        //    var inmunizations = await context.Inmunizations.Where(p => p.UserId == user.Id).ToListAsync();
        //    return mapper.Map<List<InmunizationDTO>>(inmunizations);
        //}

        [HttpGet]
        public async Task<ActionResult<List<InmunizationDTO>>> Get()
        {
            var user = await GetConectedUser();
            var inmunizations = await Context.Inmunizations.Where(p=>p.UserId == user.Id).ToListAsync();
            return Mapper.Map<List<InmunizationDTO>>(inmunizations);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InmunizationCreationDTO model)
        {
            var user = await GetConectedUser();
            
            var inmunization = Mapper.Map<Inmunization>(model);
            if (model.Photo != null)
                inmunization.CardPicture = await StorageSaver.SaveFile(container, model.Photo);
            inmunization.UserId = user.Id;

            //TODO: Here we have to make validation of the center if exist or something
           
            Context.Add(inmunization);
            await Context.SaveChangesAsync();

            return NoContent();
        }
         
    }
}
