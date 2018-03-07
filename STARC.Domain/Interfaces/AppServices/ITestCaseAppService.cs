using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.TestCase;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface ITestCaseAppService
    {
        long Add(TestCase testCase);

        void Update(TestCase testCase);

        void ChangeStatus(TestCase testCase);

        TestCaseToQueryViewModel GetById(long id);

        IEnumerable<TestCaseToQueryViewModel> GetByTestPlan(long testPlanId);

        IEnumerable<TestCaseToQueryViewModel> GetByTestSuite(long testSuiteId);

        EntityValidationResult IsValid(TestCase testCase);
    }
}
