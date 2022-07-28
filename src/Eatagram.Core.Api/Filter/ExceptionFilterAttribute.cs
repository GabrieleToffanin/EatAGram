using Eatagram.Core.Api.Filter.Common;
using Eatagram.Framework.Logger.LogSetup;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Eatagram.Core.Api.Filter;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ExceptionFilterAttribute : LoggerFilterAttributeBase 
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
            return;

        var paramJson = ActionArguments.Count == 0
            ? "-"
            : JsonSerializer.Serialize(ActionArguments);

        var messageTracing = $"({ApplicationName}|{EnvironmentName}) " +
                             $"(Exception) " +
                             $"[{HttpMethodName}] {ControllerName}/{ActionName} " +
                             $"with params '{paramJson}' (auth:{AuthenticatedUserName}) " +
                             $"- Exception: {context.Exception}";

        SeriLogger.Error(messageTracing);

        base.OnActionExecuted(context);
    }
}