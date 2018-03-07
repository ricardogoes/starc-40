using STARC.Domain.Entities;
using STARC.Domain.ViewModels.TestSuite;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.Repositories
{
    public interface ITestSuiteRepository
    {
        long Add(TestSuite testSuite);

        void Update(TestSuite testSuite);

        void Delete(long testSuiteId);

        TestSuiteToQueryViewModel GetById(long id);

        IEnumerable<TestSuiteToQueryViewModel> GetByTestPlan(long testPlanId);

        IEnumerable<TestSuiteToQueryViewModel> GetByParentTestSuite(long parentTestSuiteId);
    }
}
