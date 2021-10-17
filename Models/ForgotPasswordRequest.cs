using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
