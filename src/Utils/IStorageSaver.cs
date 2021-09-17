using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace VacunaAPI.Utils
{
    public interface IStorageSaver
    {
        Task DeleteFile(string route, string container);
        Task<string> EditFile(string container, IFormFile file, string route);
        Task<string> SaveFile(string container, IFormFile file);
    }
}