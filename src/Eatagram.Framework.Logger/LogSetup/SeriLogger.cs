using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Framework.Logger.LogSetup
{
    public static class SeriLogger
    {
        private static readonly Lazy<ILogger> _instance = new(InitializeLogger);

        private static ILogger InitializeLogger()
        {
            var file = Assembly.GetEntryAssembly()?.GetName().Name + ".log";
            const string format = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "logs", file);

            ILogger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(
                    filePath,
                    outputTemplate: format,
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Verbose,
                    retainedFileCountLimit: 15,
                    fileSizeLimitBytes: 1024 * 1024)
                .CreateLogger();

            return logger;
        }

        public static void Error(string log)
            => _instance.Value.Error(log);

        public static void Information(string log)
            => _instance.Value.Information(log);
        
    }
}
