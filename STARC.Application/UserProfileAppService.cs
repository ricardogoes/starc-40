using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace STARC.Application
{
    public class UserProfileAppService : IUserProfileAppService
    {
        private readonly IUserProfileRepository __repository;

        public UserProfileAppService(IUserProfileRepository repository)
        {
            __repository = repository;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            try
            {
                return __repository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public UserProfile GetById(int userProfileId)
        {
            try
            {
                return __repository.GetById(userProfileId);
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
