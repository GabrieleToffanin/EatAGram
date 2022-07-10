using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Eatagram.Core.Api.Filter.Common
{
    public abstract class LoggerFilterAttributeBase : ActionFilterAttribute
    {
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethodName { get; set; }
        public IDictionary<string, object> ActionArguments { get; set; }
        public IPAddress RemoteIpAddress { get; set; }
        public string AuthenticatedUserName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context is null) return;

            base.OnActionExecuting(context);

            ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            ControllerName = "<unknown>";
            ActionName = "<unknown>";
            HttpMethodName = "<unknown>";

            ControllerActionDescriptor controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (!(controllerDescriptor is null))
            {
                ControllerName = controllerDescriptor.ControllerName;
                ActionName = controllerDescriptor.ActionName;
                ActionArguments = context.ActionArguments;
                
                if(controllerDescriptor.ActionConstraints is not null)
                {
                    var single = controllerDescriptor.ActionConstraints
                        .FirstOrDefault(x => x.GetType() == (typeof(HttpMethodActionConstraint)));

                    if (single is not null)
                        HttpMethodName = ((HttpMethodActionConstraint)single)
                            .HttpMethods.SingleOrDefault();
                }
            }
            AuthenticatedUserName = context.HttpContext.User.Identity.IsAuthenticated
                ?
                context.HttpContext.User.Identity.Name
                :
                "Anonymous";

            RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress;
        }

        

    }
}
