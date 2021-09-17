using VacunaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacunaAPI.Repositories
{
    public class InMemoryRepository : IRepository
    {
        private List<Gender> _genders;
        public InMemoryRepository()
        {
            _genders = new List<Gender>()
            {
                new Gender(){Id=1,Name="Comedia"},
                new Gender(){Id=2,Name="Accion"},
            };
            _guid = Guid.NewGuid();
        }

        public Guid _guid;

        public List<Gender> GetAllGenders()
        {
            return _genders;
        }

        public async Task<Gender> GetGenderById(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            return _genders.FirstOrDefault(x => x.Id == id);
        }

        public Guid GetGuid()
        {
            return _guid;
        }

        public void CreateGender(Gender gender)
        {
            gender.Id = _genders.Count() + 1;
            _genders.Add(gender);
        }
    }
}
