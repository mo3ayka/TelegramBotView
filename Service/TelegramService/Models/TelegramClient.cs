using Telegram.Td;
using Telegram.Td.Api;
using TelegramService.Models.ClientResultHandlers;

namespace TelegramService.Models
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
            Configuration = configuration;

            Initialize(configuration);
        }

        #region Private Fields

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
        /// Хэндлер получения обновлений
        /// </summary>
        private ClientResultHandler _clientResultHandler;

        /// <summary>
        /// Директория хранения файлов клиента телеграмма
        /// </summary>
        private string _clientFilePath;

        #endregion

        #region Private Methods

        /// <summary>
        /// Инициализировать telegram клиента
        /// </summary>
        private void Initialize(IConfiguration configuration)
        {
            Client.Execute(new SetLogVerbosityLevel(configuration.GetValue<int>("TelegramLogVerbosityLevel")));

            _clientFilePath = GetTelegramPath(configuration);

            if (!Directory.Exists(_clientFilePath))
                Directory.CreateDirectory(_clientFilePath);

            if (Client.Execute(new SetLogStream(new LogStreamFile(Path.Combine(_clientFilePath, "tdlib.log"), long.MaxValue, false))) is Error error)
            {
                throw new IOException(error.Message);
            }

            _clientResultHandler = new DefaultClientResultHandler(this);
            _currentTdClient = Client.Create(_clientResultHandler);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Получить путь логирования библиотеки телеграмма
        /// </summary>
        protected abstract string GetTelegramPath(IConfiguration configuration);

        #endregion

        #region Public Methods

        /// <summary>
        /// Отправить API функцию от клиента
        /// </summary>
        /// <param name="function">Функция API Telegram'a</param>
        /// <param name="handler">Обработчик, куда придет результат выполнения функции</param>
        public void SendFunction(Function function, ClientResultHandler handler = null)
        {
            _currentTdClient.Send(function, handler ?? _clientResultHandler);
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Текущая конфигурация
        /// </summary>
        protected IConfiguration Configuration { get; }

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

        /// <summary>
        /// Обработчик обновлений клиента
        /// </summary>
        public abstract IUpdateHandler Updater { get; }

        /// <summary>
        /// Директория хранения файлов клиента телеграмма
        /// </summary>
        public string ClientDirectory => _clientFilePath;

        #endregion
    }
}
