using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.Users;
using System;

namespace STARC.Api.Controllers
{
    [ValidateModel]
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : BaseController
    {
        private readonly IUserAppService __userApp;
        private readonly IPasswordAppService __passApp;

        public UsersController(IUserAppService userApp, IPasswordAppService passApp, ILogger<UsersController> logger)
            :base(userApp, logger)
        {
            __userApp = userApp;
            __passApp = passApp;
        }

        [HttpGet("{id}", Name = "GetUserById")]        
        public IActionResult Get(long id)
        {
            try
            {
                var searchedUser = __userApp.GetById(id);

                if (searchedUser == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "User Not Found"));

                return Ok(new ApiResponse(ApiResponseState.Success, searchedUser));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("ByCustomer/{customerId}", Name = "GetUsersByCustomer")]
        public IActionResult GetByCustomer(long customerId)
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __userApp.GetByCustomer(customerId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("ByNotInProject/{projectId}", Name = "GetUsersByNotInProject")]
        public IActionResult GetByNotInProject(long projectId)
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __userApp.GetByNotInProject(projectId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody]UserToInsertViewModel userToInsert)
        {
            try
            {
                if (userToInsert == null)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "User is null"));

                if (userToInsert.UserProfileId == 1)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                var user = Mapper.Map<UserToInsertViewModel, User>(userToInsert);

                var hash = __passApp.SetCriptoPassword(user.Password);
                
                user.Password = hash.Password;
                user.PasswordHash = hash.Salt;

                user.Status = true;
                user.CreatedBy = LoggedUser.UserId;
                user.CreatedDate = DateTime.Now;
                user.LastUpdatedBy = LoggedUser.UserId;
                user.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __userApp.IsValid(user);

                if (isValidEntity.Status == false)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));

                var userIdInserted = __userApp.Add(user);

                return CreatedAtRoute("GetUserById", new { controller = "Users", id = userIdInserted }, null);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        public IActionResult Update(long id, [FromBody]UserToUpdateViewModel userToUpdate)
        {
            try
            {
                if (userToUpdate == null || userToUpdate.UserId != id)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                if (userToUpdate.UserProfileId == 1)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                var searchedUser = __userApp.GetById(id);
                if (searchedUser == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "User Not Found"));

                var user = Mapper.Map<UserToUpdateViewModel, User>(userToUpdate);

                if(!string.IsNullOrEmpty(user.Password))
                {
                    var hash = __passApp.SetCriptoPassword(user.Password);                    

                    user.Password = hash.Password;
                    user.PasswordHash = hash.Salt;
                }

                user.Username = searchedUser.Username;                    
                user.Status = searchedUser.Status;
                user.CreatedBy = searchedUser.CreatedBy;
                user.CreatedDate = searchedUser.CreatedDate;
                user.LastUpdatedBy = LoggedUser.UserId;
                user.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __userApp.IsValid(user);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                __userApp.Update(user);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}/ChangeStatus", Name = "ChangeStatusUser")]
        public IActionResult ChangeStatus(long id)
        {
            try
            {
                var searchedUser = Mapper.Map<User>(__userApp.GetById(id));

                if (searchedUser == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "User Not Found"));

                searchedUser.ChangeStatus();
                searchedUser.LastUpdatedBy = LoggedUser.UserId;
                searchedUser.LastUpdatedDate = DateTime.Now;

                __userApp.ChangeStatus(searchedUser);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }        
    }
}
