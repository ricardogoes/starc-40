using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using STARC.Domain.Interfaces.AppServices;

namespace STARC.Api.FiltersAttributes
{
    //TODO: Implementar Unit Test
    //TODO: Validate customer access level
    public class HasAuthorityAttribute : TypeFilterAttribute
    {
        public HasAuthorityAttribute() 
            : base(typeof(HasAuthority))
        {
        }

        private class HasAuthority : IActionFilter
        {
            private readonly IUserAppService __app;

            public HasAuthority(IUserAppService app)
            {
                __app = app;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                bool hasAuthority = false;
                var loggedUser = __app.GetByUsername(context.HttpContext.User.Identity.Name);
                var apiController = context.Controller.ToString();
                

                if (loggedUser.UserProfileId == 1)
                    hasAuthority = true;

                else if (loggedUser.UserProfileId == 2)
                {
                    if (apiController.Contains("CustomersController"))
                        hasAuthority = false;
                    else
                        hasAuthority = true;
                }
                else
                {
                    if (apiController.Contains("CustomersController") ||
                        apiController.Contains("UsersController") ||
                        apiController.Contains("UserProfilesController"))
                        hasAuthority = false;
                    else
                        hasAuthority = true;
                }

                if(!hasAuthority)
                    context.Result = new UnauthorizedResult();
            }
        }
    }    
}
