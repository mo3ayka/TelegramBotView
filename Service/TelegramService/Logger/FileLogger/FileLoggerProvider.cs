using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace TelegramService.Logger.FileLogger
{
    /// <summary>
    /// Файловый поставщик лога
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Узел конфигурации в appsettings для файлового лога
        /// </summary>
        public const string FileLoggerProviderSection = "FileLogger";

        /// <summary>
        /// Конфиг файлового лога
        /// </summary>
        private readonly FileLoggerConfig _config;

        /// <summary>
        /// Список созданных файловоых логеров
        /// </summary>
        private readonly ConcurrentDictionary<string, FileLogger> _loggers = new ConcurrentDictionary<string, FileLogger>();

        public FileLoggerProvider(IOptions<FileLoggerConfig> options)
        {
            _config = options.Value;

            _config.Path = Path.Combine(Environment.ExpandEnvironmentVariables(_config.Path), $"{DateTime.Now.ToString("MM_dd_yyyy_HH_mm")}.csv");
            var dir = Path.GetDirectoryName(_config.Path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            FileLogger.LogPath = _config.Path;
            FileLogger.LogWriterThread.Start();
        }
        
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new FileLogger(name));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
