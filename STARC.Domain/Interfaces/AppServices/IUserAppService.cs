using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Users;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface IUserAppService
    {
        long Add(User user);

        void Update(User user);

        void ChangeStatus(User user);

        UserToQueryViewModel GetById(long id);

        UserToQueryViewModel GetByUsernameAndPassword(string username, string password);

        UserToQueryViewModel GetByUsername(string username);

        IEnumerable<UserToQueryViewModel> GetByCustomer(long customerId);

        IEnumerable<UserToQueryViewModel> GetByNotInProject(long projectId);

        bool IsUsernameUnique(string username);

        EntityValidationResult IsValid(User user);
    }
}
