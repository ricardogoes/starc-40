using STARC.Domain.Entities;
using STARC.Domain.ViewModels.TestPlan;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.Repositories
{
    public interface ITestPlanRepository
    {
        long Add(TestPlan testPlan);

        void Update(TestPlan testPlan);

        void ChangeStatus(TestPlan testPlan);

        TestPlanToQueryViewModel GetById(long id);

        IEnumerable<TestPlanToQueryViewModel> GetByProject(long projectId);
        
        IEnumerable<TestPlanToQueryViewModel> GetActiveByProject(long projectId);

        IEnumerable<TestPlanStructureViewModel> GetStructure(long testPlanId);
    }
}
