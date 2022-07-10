namespace TelegramService.Logger.FileLogger
{
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Добавить файловый логгер как поставщика лога
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<FileLoggerConfig> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
