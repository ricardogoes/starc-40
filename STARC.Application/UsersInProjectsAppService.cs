using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.UsersInProjects;
using System;
using System.Collections.Generic;

namespace STARC.Application
{
    public class UsersInProjectsAppService : IUsersInProjectsAppService
    {
        private readonly IUsersInProjectsRepository __repository;
        private readonly IUserAppService __userApp;
        private readonly IProjectAppService __projectApp;

        public UsersInProjectsAppService(IUsersInProjectsRepository repository, IUserAppService userApp, IProjectAppService projectApp)
        {
            __repository = repository;
            __userApp = userApp;
            __projectApp = projectApp;
        }

        public long Add(UsersInProjects entity)
        {
            try
            {
                return __repository.Add(entity);
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public void Delete(long id)
        {
            try
            {
                __repository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public IEnumerable<UsersInProjectsToQueryViewModel> GetByCustomer(long customerId)
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

        public UsersInProjectsToQueryViewModel GetById(long id)
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

        public IEnumerable<UsersInProjectsToQueryViewModel> GetByProject(long projectId)
        {
            try
            {
                if (projectId == 0)
                    throw new ArgumentException("ProjectId invalid");

                return __repository.GetByProject(projectId);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public IEnumerable<UsersInProjectsToQueryViewModel> GetByUser(long userId)
        {
            try
            {
                if (userId == 0)
                    throw new ArgumentException("UserId invalid");

                return __repository.GetByUser(userId);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public bool IsUserAndProjectUnique(long userId, long projectId)
        {
            try
            {
                var userInProject = __repository.GetByUserAndProject(userId, projectId);

                if (userInProject == null)
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public EntityValidationResult IsValid(UsersInProjects userInProject)
        {
            try
            {
                var validation = new EntityValidationResult();

                validation.Status = true;
                validation.ValidationMessages = new List<string>();

                if (!IsUserAndProjectUnique(userInProject.UserId, userInProject.ProjectId))
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("User already associate to project");
                }

                var user = __userApp.GetById(userInProject.UserId);
                if (user == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("User Not Found");
                }

                var project = __projectApp.GetById(userInProject.ProjectId);
                if (project == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Project Not Found");
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
