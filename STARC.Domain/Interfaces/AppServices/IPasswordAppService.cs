using STARC.Domain.Models;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface IPasswordAppService
    {
        HashPassword SetCriptoPassword(string password);

        string GetCriptoPassword(string password, byte[] salt);

        byte[] GetHashPassword(long userId);
    }
}
