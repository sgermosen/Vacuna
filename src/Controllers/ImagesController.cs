using System;
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
    [Route("api/images")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagesController : PsBaseController
    {

        public ImagesController(UserManager<ApplicationUser> userManager, IMapper mapper,
            IStorageSaver storageSaver, ApplicationDbContext context) :
            base(userManager, mapper, storageSaver, context)
        {
        }
         
        [HttpGet("getAll")]
        public async Task<ActionResult<List<DniOrCardDTO>>> GetAll(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var user = await GetConectedUser();
            if (user.Id != userId)
                return Unauthorized();
             
            var pictures = await Context.Images
                .Include(p => p.Type)
                .Where(p => p.UserId == userId).ToListAsync();
            return Mapper.Map<List<DniOrCardDTO>>(pictures);
        }

        [HttpPost]
        public async Task<ActionResult> Post( DniOrCardDTO model)
        {
            if (model.Picture == null)
                return BadRequest();
            var user = await GetConectedUser();
            if (model.UserId != user.Id)
                return Unauthorized();
            var cardPicture = new Image { UserId = user.Id, TypeId = model.TypeId };
            
            Guid imageId = await StorageSaver.UploadBlobAsync(model.Picture, cardPicture.Container);
            cardPicture.ImageId = imageId;
            //cardPicture.ImageUrl = await StorageSaver.SaveFile(cardPicture.Container, model.PictureFile);
            Context.Images.Add(cardPicture);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<ApplicationUser> GetConectedUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            var user = await UserManager.FindByEmailAsync(email);
            return user;
        }

    }
}
