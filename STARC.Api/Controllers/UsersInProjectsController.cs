using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.UsersInProjects;
using System;
using System.Linq;

namespace STARC.Api.Controllers
{
    [ValidateModel]
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/UsersInProjects")]
    public class UsersInProjectsController : BaseController
    {
        private readonly IUsersInProjectsAppService __app;
        private readonly IUserAppService __userApp;
        private readonly IProjectAppService __projectApp;

        public UsersInProjectsController(IUsersInProjectsAppService app, IUserAppService userApp, IProjectAppService projectApp, ILogger<UsersInProjectsController> logger)
            : base(userApp, logger)
        {
            __app = app;
            __userApp = userApp;
            __projectApp = projectApp;
        }

        [HttpGet("{id}", Name = "GetUserInProjectById")]
        public IActionResult Get(long id)
        {
            try
            {
                var searchedUser = __app.GetById(id);

                if (searchedUser == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "User in project not found"));

                return Ok(new ApiResponse(ApiResponseState.Success, searchedUser));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }

        [HttpGet("ByUser/{userId}", Name = "GetUsersInProjectsByUser")]
        public IActionResult GetByUser(long userId)
        {
            try
            {
                var user = __userApp.GetById(userId);

                if (user == null || user.CustomerId != LoggedUser.CustomerId)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "User not found"));

                return Ok(new ApiResponse(ApiResponseState.Success, __app.GetByUser(userId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }

        [HttpGet("ByProject/{projectId}", Name = "GetUsersInProjectsByProject")]
        public IActionResult GetByProject(long projectId)
        {
            try
            {
                var project = __projectApp.GetById(projectId);

                if (project == null || project.CustomerId != LoggedUser.CustomerId)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Project not found"));

                var usersInProjects = __app.GetByProject(projectId).ToList();
                return Ok(new ApiResponse(ApiResponseState.Success, usersInProjects));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }

        [HttpGet("ByCustomer/{customerId}", Name = "GetUsersInProjectsByCustomer")]
        public IActionResult GetByCustomer(long customerId)
        {
            try
            {
                if (LoggedUser.UserProfileId != 1 && customerId != LoggedUser.CustomerId)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Customer not found"));

                return Ok(new ApiResponse(ApiResponseState.Success, __app.GetByCustomer(customerId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }

        [HttpPost(Name = "InsertUserInProject")]
        public IActionResult Insert([FromBody]UsersInProjectsToInsertViewModel userInProjectToInsert)
        {
            try
            {
                if (userInProjectToInsert == null)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "User in project is null"));

                var userInProject = Mapper.Map<UsersInProjectsToInsertViewModel, UsersInProjects>(userInProjectToInsert);
                userInProject.CreatedBy = LoggedUser.UserId;
                userInProject.CreatedDate = DateTime.Now;

                var isValidEntity = __app.IsValid(userInProject);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                var userInProjectIdInserted = __app.Add(userInProject);

                return CreatedAtRoute("GetProjectById", new { controller = "UsersInProjects", id = userInProjectIdInserted }, null);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var userInProject = __app.GetById(id);

                if (userInProject == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "User in Project Not Found"));

                __app.Delete(id);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }
    }
}