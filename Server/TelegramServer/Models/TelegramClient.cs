using Telegram.Td;
using Telegram.Td.Api;
using TelegramServer.Models.ClientResultHandlers;

namespace TelegramServer.Models
{
    /// <summary>
    /// Обертка над клиентом телеграмма с инициализацией подключения
    /// </summary>
    public abstract class TelegramClient
    {
        /// <summary>
        /// Базовый конструктор для создания telegram клиента(пользователь/бот)
        /// </summary>
        /// <param name="logger">Логер сервера</param>
        /// <param name="configuration">Конфигурация сервера</param>
        /// <param name="apiId">Идентификатор Api telegram'a</param>
        /// <param name="apiHash">Хэш Api telegram'a</param>
        public TelegramClient(ILogger logger, IConfiguration configuration, int apiId, string apiHash)
        {
            _logger = logger;
            _apiId = apiId;
            _apiHash = apiHash;

            Initialize(configuration);
        }

        #region Protected Fields

        /// <summary>
        /// Идентификатор api telegram'a
        /// </summary>
        private int _apiId;

        /// <summary>
        /// Хэш api telegram'a
        /// </summary>
        private string _apiHash;

        /// <summary>
        /// Текущий td client
        /// </summary>
        private Client _currentTdClient;

        /// <summary>
        /// Логгер
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// Текущий td бот
        /// </summary>
        private Client _currentBotClient;

        /// <summary>
        /// Хэндлер получения обновлений
        /// </summary>
        private ClientResultHandler _clientResultHandler;

        #endregion

        #region Private Methods

        /// <summary>
        /// Инициализировать telegram клиента
        /// </summary>
        private void Initialize(IConfiguration configuration)
        {
            Client.Execute(new SetLogVerbosityLevel(configuration.GetValue<int>("TelegramLogVerbosityLevel")));

            var telegramPath = GetTelegramPath(configuration);

            if (!Directory.Exists(telegramPath))
                Directory.CreateDirectory(telegramPath);

            if (Client.Execute(new SetLogStream(new LogStreamFile(Path.Combine(telegramPath, "tdlib.log"), long.MaxValue, false))) is Error error)
            {
                throw new IOException(error.Message);
            }

            _clientResultHandler = new DefaultClientResultHandler(this);
            _currentBotClient = Client.Create(_clientResultHandler);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Получить путь логирования библиотеки телеграмма
        /// </summary>
        protected abstract string GetTelegramPath(IConfiguration configuration);

        #endregion

        #region Public Properties

        /// <summary>
        /// Идентификатор api telegram'a
        /// </summary>
        public int ApiId => _apiId;

        /// <summary>
        /// Хэш идентификатора api telegram'a
        /// </summary>
        public string ApiHash => _apiHash;

        /// <summary>
        /// Логгер
        /// </summary>
        public ILogger Logger => _logger;

        #endregion
    }
}
