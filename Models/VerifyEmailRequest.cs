using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.Models
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
