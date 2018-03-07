using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.UsersInProjects;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface IUsersInProjectsAppService
    {
        long Add(UsersInProjects entity);

        void Delete(long id);

        UsersInProjectsToQueryViewModel GetById(long id);

        IEnumerable<UsersInProjectsToQueryViewModel> GetByUser(long userId);

        IEnumerable<UsersInProjectsToQueryViewModel> GetByProject(long projectId);

        IEnumerable<UsersInProjectsToQueryViewModel> GetByCustomer(long customerId);

        bool IsUserAndProjectUnique(long userId, long projectId);

        EntityValidationResult IsValid(UsersInProjects userInProject);
    }
}
