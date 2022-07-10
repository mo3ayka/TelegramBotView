using System.Collections.Concurrent;

namespace TelegramService.Logger.FileLogger
{
    /// <summary>
    /// Файловый логгер
    /// </summary>
    public class FileLogger : ILogger
    {
        /// <summary>
        /// Имя логгера
        /// </summary>
        private readonly string _name;

        public FileLogger(string name)
        {
            _name = name;
        }

        #region ILogger

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            _logQueue.Add((DateTime.Now.ToString(), logLevel, eventId.Id, _name, formatter(state, exception).Replace(Environment.NewLine, ""), exception?.Message));
        }

        #endregion

        #region Static Log Writer

        /// <summary>
        /// Очередь на запись в лог
        /// </summary>
        private static BlockingCollection<(string TimeLog, LogLevel LogLevel, int EventId, string NameLog, string Formatter, string exMessage)> _logQueue =
            new BlockingCollection<(string, LogLevel, int, string, string, string)>();

        /// <summary>
        /// Поток записи в лог
        /// </summary>
        public static Thread LogWriterThread = new Thread(WriteLog)
        {
            Name = "ServerLogWriter",
            IsBackground = true
        };

        /// <summary>
        /// Путь до файла логов
        /// </summary>
        public static string LogPath;

        /// <summary>
        /// Запись в лог
        /// </summary>
        private static void WriteLog()
        {
            foreach (var log in _logQueue.GetConsumingEnumerable())
            {
                var content = $"{log.TimeLog};{log.LogLevel};{log.EventId};{log.NameLog};{log.Formatter};{log.exMessage}{Environment.NewLine}";

                File.AppendAllText(LogPath, content);
            }
        }

        #endregion
    }
}
