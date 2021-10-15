using VacunaAPI.Models;

namespace VacunaAPI.Utils
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
