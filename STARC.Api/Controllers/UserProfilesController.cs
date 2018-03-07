using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Interfaces.AppServices;
using System;

namespace STARC.Api.Controllers
{
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/UserProfiles")]
    public class UserProfilesController : BaseController
    {
        private readonly IUserProfileAppService __app;

        public UserProfilesController(IUserProfileAppService app, IUserAppService userApp, ILogger<UserProfilesController> logger)
            :base(userApp, logger)
        {
            __app = app;
        }

        [HttpGet("", Name = "GetAllUserProfiles")]
        public IActionResult Get()
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __app.GetAll()));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("{id}", Name = "GetUserProfileById")]
        public IActionResult Get(int id)
        {
            try
            {
                // Do not return System Administrator
                if (id == 1)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Profile not Found"));

                var searchedProfile = __app.GetById(id);

                if (searchedProfile == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Profile not Found"));

                return new ObjectResult(searchedProfile);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}