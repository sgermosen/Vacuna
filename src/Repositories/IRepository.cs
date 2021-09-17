using VacunaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VacunaAPI.Repositories
{
    public interface IRepository
    {
        void CreateGender(Gender gender);
        List<Gender> GetAllGenders();
        Task<Gender> GetGenderById(int id);
        Guid GetGuid();
    }
}
