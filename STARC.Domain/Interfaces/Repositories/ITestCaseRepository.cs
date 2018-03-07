using STARC.Domain.Entities;
using STARC.Domain.ViewModels.TestCase;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.Repositories
{
    public interface ITestCaseRepository
    {
        long Add(TestCase testCase);

        void Update(TestCase testCase);

        void ChangeStatus(TestCase testCase);

        TestCaseToQueryViewModel GetById(long id);

        IEnumerable<TestCaseToQueryViewModel> GetByTestPlan(long testPlanId);

        IEnumerable<TestCaseToQueryViewModel> GetByTestSuite(long testSuiteId);
    }
}
