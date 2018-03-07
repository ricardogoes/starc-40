using STARC.Domain.Entities;
using STARC.Domain.ViewModels.Users;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        long Add(User user);

        void Update(User user);

        void ChangeStatus(User user);

        UserToQueryViewModel GetById(long id);

        UserToQueryViewModel GetByUsernameAndPassword(string username, string password);

        UserToQueryViewModel GetByUsername(string username);

        IEnumerable<UserToQueryViewModel> GetByCustomer(long customerId);

        IEnumerable<UserToQueryViewModel> GetByNotInProject(long projectId);
    }
}
