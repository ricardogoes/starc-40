using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.TestPlan;
using System;
using System.Collections.Generic;

namespace STARC.Application
{
    public class TestPlanAppService : ITestPlanAppService
    {
        private readonly ITestPlanRepository __repository;
        private readonly IProjectAppService __projectService;
        private readonly IUserAppService __userService;

        public TestPlanAppService(ITestPlanRepository repository, IProjectAppService projectService, IUserAppService userService)
        {
            __repository = repository;
            __projectService = projectService;
            __userService = userService;
        }

        public long Add(TestPlan testPlan)
        {
            try
            {
                return __repository.Add(testPlan);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TestPlan testPlan)
        {
            try
            {
                __repository.Update(testPlan);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(TestPlan testPlan)
        {
            try
            {
                __repository.ChangeStatus(testPlan);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestPlanToQueryViewModel> GetActiveByProject(long projectId)
        {
            try
            {
                return __repository.GetActiveByProject(projectId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<TestPlanToQueryViewModel> GetByProject(long projectId)
        {
            try
            {
                return __repository.GetByProject(projectId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TestPlanToQueryViewModel GetById(long id)
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

        public IEnumerable<TestPlanStructureViewModel> GetStructure(long testPlanId)
        {
            //TODO: Add Tests
            try
            {
                return __repository.GetStructure(testPlanId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EntityValidationResult IsValid(TestPlan testPlan)
        {
            try
            {
                if (testPlan == null)
                    throw new ArgumentException("Test Plan invalid");

                var validation = new EntityValidationResult();

                validation.Status = true;
                validation.ValidationMessages = new List<string>();

                if (testPlan.StartDate > testPlan.FinishDate)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Finish Date must be greater than Start Date");
                }

                var project = __projectService.GetById(testPlan.ProjectId);
                if (project == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Project Not Found");
                }

                if (testPlan.OwnerId.HasValue)
                {
                    var user = __userService.GetById(testPlan.OwnerId.Value);
                    if (user == null)
                    {
                        validation.Status = false;
                        validation.ValidationMessages.Add("Owner Not Found");
                    }
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
