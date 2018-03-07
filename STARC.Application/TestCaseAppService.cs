using System;
using System.Collections.Generic;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.TestCase;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.Models;

namespace STARC.Application
{
    public class TestCaseAppService : ITestCaseAppService
    {
        private readonly ITestCaseRepository __repository;

        private readonly ITestSuiteAppService __testSuiteApp;

        public TestCaseAppService(ITestCaseRepository repository, ITestSuiteAppService testSuiteApp)
        {
            __repository = repository;
            __testSuiteApp = testSuiteApp;
        }

        public long Add(TestCase testCase)
        {
            try
            {
                return __repository.Add(testCase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TestCase testCase)
        {
            try
            {
                __repository.Update(testCase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(TestCase testCase)
        {
            try
            {
                __repository.ChangeStatus(testCase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestCaseToQueryViewModel> GetByTestPlan(long testPlanId)
        {
            try
            {
                return __repository.GetByTestPlan(testPlanId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestCaseToQueryViewModel> GetByTestSuite(long testSuiteId)
        {
            //TODO: Add tests
            try
            {
                return __repository.GetByTestSuite(testSuiteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TestCaseToQueryViewModel GetById(long id)
        {
            try
            {
                return __repository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EntityValidationResult IsValid(TestCase testCase)
        {
            try
            {
                var validation = new EntityValidationResult();

                validation.Status = true;
                validation.ValidationMessages = new List<string>();

                var testSuite = __testSuiteApp.GetById(testCase.TestSuiteId);
                if (testSuite == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Test Suite Not Found");
                }

                return validation;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
