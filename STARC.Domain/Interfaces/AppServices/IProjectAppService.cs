using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Projects;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface IProjectAppService
    {
        long Add(Project project);

        void Update(Project project);

        void ChangeStatus(Project project);

        ProjectToQueryViewModel GetById(long id);

        IEnumerable<ProjectToQueryViewModel> GetByCustomer(long customerId);

        IEnumerable<ProjectToQueryViewModel> GetActiveByCustomer(long customerId);

        EntityValidationResult IsValid(Project project);
    }
}
