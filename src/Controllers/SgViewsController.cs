using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VacunaAPI.DTOs;
using VacunaAPI.Entities;
using VacunaAPI.Utils;

namespace VacunaAPI.Controllers
{
    public class SgViewsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMailHelper mailHelper;

        public SgViewsController(UserManager<ApplicationUser> userManager,
      IMapper mapper, ApplicationDbContext context, IConfiguration configuration,
      SignInManager<ApplicationUser> signInManager, IMailHelper mailHelper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mailHelper = mailHelper;
        }

        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPasswordDTO { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var user = await context.Users.Where(p => p.Email == model.UserName).FirstOrDefaultAsync();
            if (user != null)
            {
                IdentityResult result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Contaseña cambiada.";
                    return View();
                }

                ViewBag.Message = "Error cambiando la contraseña.";
                return View(model);
            }

            ViewBag.Message = "Usuario no encontrado.";
            return View(model);
        }
    }
}
