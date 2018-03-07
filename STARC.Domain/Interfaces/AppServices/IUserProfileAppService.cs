using STARC.Domain.Entities;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface IUserProfileAppService
    {
        UserProfile GetById(int userProfileId);

        IEnumerable<UserProfile> GetAll();        
    }
}
