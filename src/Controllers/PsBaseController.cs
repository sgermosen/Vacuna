using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacunaAPI.Entities;
using VacunaAPI.Utils;

namespace VacunaAPI.Controllers
{
    //[ApiController]
    // [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class PsBaseController : Controller
    {
        public readonly UserManager<ApplicationUser> UserManager;
        public readonly ApplicationDbContext Context;
        public readonly IMapper Mapper;
        public readonly IStorageSaver StorageSaver;

        public PsBaseController(UserManager<ApplicationUser> userManager,
          IMapper mapper,
          IStorageSaver storageSaver,
            ApplicationDbContext context)
        {
            this.UserManager = userManager;
            this.Mapper = mapper;
            this.StorageSaver = storageSaver;
            this.Context = context;
        }

        public PsBaseController(UserManager<ApplicationUser> userManager,
         IMapper mapper,
           ApplicationDbContext context)
        {
            this.UserManager = userManager;
            this.Mapper = mapper;
            this.Context = context;
        }


        //public async Task<IdentityUser> GetConectedUser()
        //{
        //    var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
        //    var user = await UserManager.FindByEmailAsync(email);
        //    return user;
        //}
    }
}
