namespace STARC.Domain.Interfaces.Repositories
{
    public interface IPasswordRepository
    {
        byte[] GetHashPassword(long userId);
    }
}
