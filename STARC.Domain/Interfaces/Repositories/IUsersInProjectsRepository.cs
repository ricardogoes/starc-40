using STARC.Domain.Entities;
using STARC.Domain.ViewModels.UsersInProjects;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.Repositories
{
    public interface IUsersInProjectsRepository
    {
        long Add(UsersInProjects entity);

        void Delete(long id);

        UsersInProjectsToQueryViewModel GetById(long id);

        UsersInProjectsToQueryViewModel GetByUserAndProject(long userId, long projectId);

        IEnumerable<UsersInProjectsToQueryViewModel> GetByUser(long userId);

        IEnumerable<UsersInProjectsToQueryViewModel> GetByProject(long projectId);

        IEnumerable<UsersInProjectsToQueryViewModel> GetByCustomer(long customerId);
    }
}
