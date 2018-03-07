using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Users;
using System;
using System.Collections.Generic;

namespace STARC.Application
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository __repository;
        private readonly IUserProfileAppService __userProfileApp;
        private readonly ICustomerAppService __customerApp;

        public UserAppService(IUserRepository repository, IUserProfileAppService userProfileApp, ICustomerAppService customerApp)             
        {
            __repository = repository;
            __userProfileApp = userProfileApp;
            __customerApp = customerApp;
        }

        public long Add(User user)
        {
            try
            {
                return __repository.Add(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(User user)
        {
            try
            {
                __repository.Update(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(User user)
        {
            try
            {
                __repository.ChangeStatus(user);
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public IEnumerable<UserToQueryViewModel> GetByCustomer(long customerId)
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

        public UserToQueryViewModel GetById(long id)
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

        public IEnumerable<UserToQueryViewModel> GetByNotInProject(long projectId)
        {
            try
            {
                if (projectId == 0)
                    throw new ArgumentException("ProjectId invalid");

                return __repository.GetByNotInProject(projectId);
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public UserToQueryViewModel GetByUsername(string username)
        {
            try
            {
                return __repository.GetByUsername(username);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public UserToQueryViewModel GetByUsernameAndPassword(string username, string password)
        {
            try
            {
                return __repository.GetByUsernameAndPassword(username, password);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsUsernameUnique(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    throw new ArgumentException("username invalid");

                var user = GetByUsername(username);

                if (user == null)
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public EntityValidationResult IsValid(User user)
        {
            try
            {
                var validation = new EntityValidationResult();

                validation.Status = true;
                validation.ValidationMessages = new List<string>();

                if (user.UserId == 0 && !IsUsernameUnique(user.Username))
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Username already exists");
                }

                var userProfile = __userProfileApp.GetById(user.UserProfileId.Value);
                if (userProfile == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Profile Not Found");
                }
            
                var customer = __customerApp.GetById(user.CustomerId.Value);
                if (customer == null)
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("Customer Not Found");
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
