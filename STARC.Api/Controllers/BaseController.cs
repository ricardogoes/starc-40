using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.Users;
using System;
using System.Security.Cryptography;

namespace STARC.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        private readonly IUserAppService __userApp;
        private readonly ILogger __logger;

        public BaseController(IUserAppService userApp, ILogger logger)
        {
            __userApp = userApp;
            __logger = logger;
        }

        protected UserToQueryViewModel LoggedUser
        {
            get
            {
                if (User == null)
                    return __userApp.GetById(1);
                else
                    return __userApp.GetByUsername(User.Identity.Name);
            }
        }

        protected IActionResult HandleError(Exception ex)
        {
            //TODO: Configure NLog properly
            __logger.LogError(ex.Message);
            return StatusCode(500, new ApiResponse(ApiResponseState.Failed, "An error ocurred, please try again. If the error persists contact the System Administrator"));
        }        
    }
}