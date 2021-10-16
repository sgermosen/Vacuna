using Microsoft.AspNetCore.Http;

namespace VacunaAPI.DTOs
{
    public class DniOrCardDTO
    {
        public string UserId { get; set; }
        public int TypeId { get; set; }
        public byte[] Picture { get; set; }
        //  public IFormFile Picture { get; set; }
        public IFormFile PictureFile { get; set; }

    }
}
