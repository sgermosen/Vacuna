using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacunaAPI.DTOs;
using VacunaAPI.Entities;

namespace VacunaAPI.Controllers
{
    [Route("api/laboratories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LaboratoriesController : PsBaseController
    {
        public LaboratoriesController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context) : base(userManager, mapper, context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<LaboratoryDTO>>> Get()
        {
            var listOfVaccines = new List<LaboratoryDTO>();
            var laboratories = await Context.Laboratories.AsNoTracking().ToListAsync();
            if (laboratories == null)
                return NotFound();

            foreach (var laboratory in laboratories)
            {
                listOfVaccines.Add(new LaboratoryDTO { LaboratoryId = laboratory.Id, LaboratoryName = laboratory.Name });
            }

            return Ok(laboratories);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LaboratoryDTO model)
        {
            var laboratory = new Laboratory { Name = model.LaboratoryName };
            Context.Add(laboratory);
            await Context.SaveChangesAsync();

            return NoContent();
        }


    }
}
