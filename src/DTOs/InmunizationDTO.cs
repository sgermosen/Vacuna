using System;
using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.DTOs
{
    public class InmunizationDTO
    {
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

        public string Photo { get; set; }

    }
}
