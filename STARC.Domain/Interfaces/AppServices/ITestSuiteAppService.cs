using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.TestSuite;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface ITestSuiteAppService
    {
        long Add(TestSuite testSuite);

        void Update(TestSuite testSuite);

        void Delete(long testSuiteId);

        TestSuiteToQueryViewModel GetById(long id);

        IEnumerable<TestSuiteToQueryViewModel> GetByTestPlan(long testPlanId);

        IEnumerable<TestSuiteToQueryViewModel> GetByParentTestSuite(long parentTestSuiteId);

        EntityValidationResult IsValid(TestSuite testSuite);
    }
}
