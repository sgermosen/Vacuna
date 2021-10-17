using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.Models
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
