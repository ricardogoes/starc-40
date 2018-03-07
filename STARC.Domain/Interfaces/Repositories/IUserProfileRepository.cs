using STARC.Domain.Entities;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetById(int userProfileId);

        IEnumerable<UserProfile> GetAll();
    }
}
