using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.TestPlan;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface ITestPlanAppService
    {
        long Add(TestPlan testPlan);

        void Update(TestPlan testPlan);

        void ChangeStatus(TestPlan testPlan);

        TestPlanToQueryViewModel GetById(long id);

        IEnumerable<TestPlanToQueryViewModel> GetByProject(long projectId);

        IEnumerable<TestPlanToQueryViewModel> GetActiveByProject(long projectId);

        IEnumerable<TestPlanStructureViewModel> GetStructure(long testPlanId);

        EntityValidationResult IsValid(TestPlan testPlan);
    }
}
