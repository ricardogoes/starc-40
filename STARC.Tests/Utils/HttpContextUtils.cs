using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace STARC.Tests.Utils
{
    public class HttpContextUtils
    {
        public static ActionExecutingContext MockedActionExecutingContext(
            HttpContext context,
            IList<IFilterMetadata> filters,
            IDictionary<string, object> actionArguments,
            object controller
        )
        {
            var actionContext = new ActionContext() { HttpContext = context };

            return new ActionExecutingContext(actionContext, filters, actionArguments, controller);
        }
        public static ActionExecutingContext MockedActionExecutingContext(
            HttpContext context,
            object controller
        )
        {
            return MockedActionExecutingContext(context, new List<IFilterMetadata>(), new Dictionary<string, object>(), controller);
        }
    }
}
