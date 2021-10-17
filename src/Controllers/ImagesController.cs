using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<ActionResult> Post([FromForm] DniOrCardDTO model)
        {
            if (model.Picture == null && model.PictureFile == null)
                return BadRequest();
            var user = await GetConectedUser();
            if (model.UserId != user.Id)
                return Unauthorized();
            var cardPicture = new Image { UserId = user.Id, TypeId = model.TypeId };
            if (model.Picture != null)
            {
                Guid imageId = await StorageSaver.UploadBlobAsync(model.Picture, cardPicture.Container);
                cardPicture.ImageId = imageId;
            }
            if (model.PictureFile != null)
                cardPicture.ImageUrl = await StorageSaver.SaveFile(cardPicture.Container, model.PictureFile);
            Context.Images.Add(cardPicture);
            await Context.SaveChangesAsync();

            return Ok(cardPicture);
        }

        private async Task<ApplicationUser> GetConectedUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            if (string.IsNullOrEmpty(email))
                return new ApplicationUser();
            var user = await UserManager.FindByEmailAsync(email);
            return user;
        }

    }
}
