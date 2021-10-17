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
    [Route("api/vaccine")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VaccinesController : PsBaseController
    {
        public VaccinesController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context) : base(userManager, mapper, context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<VaccineDTO>>> Get()
        {
            var listOfVaccines = new List<VaccineDTO>();
            var vaccines = await Context.Vaccines.AsNoTracking().ToListAsync();
            if (vaccines == null)
                return NotFound();

            foreach (var vaccine in vaccines)
            {
                listOfVaccines.Add(new VaccineDTO { VaccineId = vaccine.Id, VaccineName = vaccine.Name });
            }

            return Ok(vaccines);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VaccineDTO model)
        {
            var vaccine = new Vaccine { Name = model.VaccineName };
            Context.Add(vaccine);
            await Context.SaveChangesAsync();

            return NoContent();
        }


    }
}
