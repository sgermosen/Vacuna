
namespace VacunaAPI.DTOs
{
    public class InmunizationValidDTO
    {
        public string Identification { get; set; }

        public string FullName { get; set; }

        public string VaccineName { get; set; }

        public string LaboratoryName { get; set; }

        public string Lote { get; set; }

        public bool IsValidatedByMinistry { get; set; }


    }
}
