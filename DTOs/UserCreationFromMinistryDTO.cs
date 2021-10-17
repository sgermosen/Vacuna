using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.DTOs
{
    public class UserCreationFromMinistryDTO : UserCreationDTO
    {
        [Required]
        public int VacunationCenterId { get; set; }
    }
}
