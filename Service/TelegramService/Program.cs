using TelegramService.Logger.FileLogger;

namespace TelegramService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build();

            app.Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostBuilderContext, logging) =>
                {
                    logging.AddFileLogger(options =>
                    {
                        hostBuilderContext
                        .Configuration
                        .GetSection($"Logging:{FileLoggerProvider.FileLoggerProviderSection}")
                        .Bind(options);
                    });
                });
    }
}
