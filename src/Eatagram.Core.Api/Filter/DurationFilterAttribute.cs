using Eatagram.Core.Api.Filter.Common;
using Eatagram.Framework.Logger.LogSetup;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Eatagram.Core.Api.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class DurationFilterAttribute : LoggerFilterAttributeBase
    {
        private DateTime StartTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            StartTime = DateTime.UtcNow;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            var duration = DateTime.UtcNow.Subtract(StartTime);

            var paramJson = ActionArguments == null || ActionArguments.Count == 0
                ? "-"
                : JsonSerializer.Serialize(ActionArguments);

            string messageTracing = $"({ApplicationName}|{EnvironmentName}) " +
                $"(Duration) " +
                $"[{HttpMethodName}] {ControllerName}/{ActionName} " +
                $"with params '{paramJson}' (auth:{AuthenticatedUserName}) " +
                $"- IP: {RemoteIpAddress} " +
                $"- Duration: {duration.TotalMilliseconds.ToString().Replace(",", ".")} ms.";

            SeriLogger.Information(messageTracing);

            base.OnActionExecuted(context);
        }

    }
}
