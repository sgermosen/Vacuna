using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using System.Linq;
using System.Threading.Tasks; 
using VacunaAPI.Entities;
using VacunaAPI.Utils;

namespace VacunaAPI.Controllers
{ 
   [ApiController] 
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class PsBaseController : ControllerBase
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
         
        public async Task<IdentityUser> GetConectedUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await UserManager.FindByEmailAsync(email);
            return user;
        }
    }
}
