using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Projects;
using System;
using System.Collections.Generic;

namespace STARC.Application
{
    public class ProjectAppService : IProjectAppService
    {
        private readonly IProjectRepository __repository;
        private readonly ICustomerAppService __customerService;
        private readonly IUserAppService __userService;

        public ProjectAppService(IProjectRepository repository, ICustomerAppService customerService, IUserAppService userService)
        {
            __repository = repository;
            __customerService = customerService;
            __userService = userService;
        }

        public long Add(Project project)
        {
            try
            {
                return __repository.Add(project);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Project project)
        {
            try
            {
                __repository.Update(project);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(Project project)
        {
            try
            {
                __repository.ChangeStatus(project);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public IEnumerable<ProjectToQueryViewModel> GetActiveByCustomer(long customerId)
        {
            try
            {
                return __repository.GetActiveByCustomer(customerId);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public IEnumerable<ProjectToQueryViewModel> GetByCustomer(long customerId)
        {
            try
            {
                if (customerId == 0)
                    throw new ArgumentException("CustomerId invalid");

                return __repository.GetByCustomer(customerId);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public ProjectToQueryViewModel GetById(long id)
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

        public EntityValidationResult IsValid(Project project)
        {
            try
            {
                if (project == null)
                    throw new ArgumentException("Project invalid");

                var validation = new EntityValidationResult();

                validation.Status = true;
                validation.ValidationMessages = new List<string>();

                if (project.StartDate > project.FinishDate)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Finish Date must be greater than Start Date");
                }

                var customer = __customerService.GetById(project.CustomerId);
                if (customer == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Customer Not Found");
                }

                if (project.OwnerId.HasValue)
                {
                    var user = __userService.GetById(project.OwnerId.Value);
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
