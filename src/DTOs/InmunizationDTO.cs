using System;
using System.ComponentModel.DataAnnotations;

namespace VacunaAPI.DTOs
{
    public class InmunizationDTO
    {
        [Required]
        [StringLength(300)]
        public string VacunationCenter { get; set; }
         
        public int LaboratoryId { get; set; }
        public string LaboratoryName { get; set; }

        public int VaccineId { get; set; }
        public string VaccineName { get; set; }

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
