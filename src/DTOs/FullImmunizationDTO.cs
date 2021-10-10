using System.Collections.Generic;

namespace VacunaAPI.DTOs
{
    public class FullImmunizationDTO
    {
        public UserDTO Person { get; set; }
        public List<ImmunizationDTO> Immunizations { get; set; }
    }
}
