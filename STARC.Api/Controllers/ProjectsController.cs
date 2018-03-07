using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.Projects;
using System;

namespace STARC.Api.Controllers
{
    [ValidateModel]
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : BaseController
    {
        private readonly IProjectAppService __projectApp;

        public ProjectsController(IProjectAppService projectApp, IUserAppService userApp, ILogger<ProjectsController> logger)
            :base(userApp, logger)
        {
            __projectApp = projectApp;
        }

        [HttpGet("Active/ByCustomer/{customerId}", Name = "GetActiveProjectsByCustomer")]
        public IActionResult GetActiveByCustomer(long customerId)
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __projectApp.GetActiveByCustomer(customerId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("ByCustomer/{customerId}", Name = "GetProjectsByCustomer")]
        public IActionResult GetByCustomer(long customerId)
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __projectApp.GetByCustomer(customerId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("{id}", Name = "GetProjectById")]
        public IActionResult Get(long id)
        {
            try
            {
                var searchedProject = __projectApp.GetById(id);

                if (searchedProject == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Project not Found"));

                return Ok(new ApiResponse(ApiResponseState.Success, searchedProject));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost(Name ="InsertProject")]
        public IActionResult Insert([FromBody]ProjectToInsertViewModel projectToInsert)
        {
            try
            {
                if (projectToInsert == null)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Project is null"));

                var project = Mapper.Map<ProjectToInsertViewModel, Project>(projectToInsert);
                project.Status = true;
                project.CreatedBy = LoggedUser.UserId;
                project.CreatedDate = DateTime.Now;
                project.LastUpdatedBy = LoggedUser.UserId;
                project.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __projectApp.IsValid(project);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                var projectIdInserted = __projectApp.Add(project);

                return CreatedAtRoute("GetProjectById", new { controller = "Projects", id = projectIdInserted }, null);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}", Name = "UpdateProject")]
        public IActionResult Update(long id, [FromBody]ProjectToUpdateViewModel projectToUpdate)
        {
            try
            {
                if (projectToUpdate == null || projectToUpdate.ProjectId != id)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                var searchedProject = __projectApp.GetById(id);
                if (searchedProject == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Project Not Found"));

                var project = Mapper.Map<ProjectToUpdateViewModel, Project>(projectToUpdate);
                project.Status = searchedProject.Status;
                project.CreatedBy = searchedProject.CreatedBy;
                project.CreatedDate = searchedProject.CreatedDate;
                project.LastUpdatedBy = LoggedUser.UserId;
                project.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __projectApp.IsValid(project);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                __projectApp.Update(project);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}/ChangeStatus", Name = "ChangeStatusProject")]
        public IActionResult ChangeStatus(long id)
        {
            try
            {
                var searchedProject = Mapper.Map<Project>(__projectApp.GetById(id));

                if (searchedProject == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Project Not Found"));

                searchedProject.ChangeStatus();
                searchedProject.LastUpdatedBy = LoggedUser.UserId;
                searchedProject.LastUpdatedDate = DateTime.Now;

                __projectApp.ChangeStatus(searchedProject);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}