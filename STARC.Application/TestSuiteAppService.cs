using System;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.TestSuite;
using STARC.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace STARC.Application
{
    public class TestSuiteAppService : ITestSuiteAppService
    {
        private readonly ITestSuiteRepository __repository;
        private readonly ITestPlanAppService __testPlanApp;

        public TestSuiteAppService(ITestSuiteRepository repository, ITestPlanAppService testPlanApp)
        {
            __repository = repository;
            __testPlanApp = testPlanApp;
        }

        public long Add(TestSuite testSuite)
        {
            try
            {
                return __repository.Add(testSuite);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TestSuite testSuite)
        {
            try
            {
                __repository.Update(testSuite);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(long testSuiteId)
        {
            try
            {
                __repository.Delete(testSuiteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TestSuiteToQueryViewModel GetById(long id)
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

        public IEnumerable<TestSuiteToQueryViewModel> GetByTestPlan(long testPlanId)
        {
            //TODO: Add tests
            try
            {
                return __repository.GetByTestPlan(testPlanId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TestSuiteToQueryViewModel> GetByParentTestSuite(long parentTestSuiteId)
        {
            //TODO: Add tests
            try
            {
                return __repository.GetByParentTestSuite(parentTestSuiteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EntityValidationResult IsValid(TestSuite testSuite)
        {
            try
            {
                var validation = new EntityValidationResult();

                validation.Status = true;
                validation.ValidationMessages = new List<string>();

                var testPlan = __testPlanApp.GetById(testSuite.TestPlanId);
                if (testPlan == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Test Plan Not Found");
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
