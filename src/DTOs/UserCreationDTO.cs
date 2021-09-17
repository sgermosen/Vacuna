using System;
using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.DTOs
{
    public class UserCreationDTO : UserCredentials
    {
        [Required]
        [StringLength(300)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string Identification { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }

        public DateTime BornDate { get; set; }
    }
}
