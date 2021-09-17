using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(300)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string Identification { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }

        public DateTime BornDate { get; set; }

        public string IdentificationPicture { get; set; }

        public bool IsFromMinistry { get; set; }
        public int? VacunationCenterId { get; set; }
        public VacunationCenter vacunationCenter { get; set; }

        public ICollection<Inmunization> Inmunizations { get; set; }

    }
}
