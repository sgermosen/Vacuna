using System;
using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.Entities
{
    public class Inmunization
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
         
        [Required]
        [StringLength(300)]
        public string VacunationCenter { get; set; }

        [Required]
        [StringLength(50)]
        public string Laboratory { get; set; }

        [Required]
        [StringLength(50)]
        public string Lote { get; set; }

        [StringLength(300)]
        public string Inmunizator { get; set; }

        public DateTime Date { get; set; }
        public string Barcode { get; set; }
        public string CardPicture { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool WasValidated { get; set; }

    }
}
