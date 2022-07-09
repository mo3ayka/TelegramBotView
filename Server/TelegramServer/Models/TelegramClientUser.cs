namespace TelegramServer.Models
{
    /// <summary>
    /// Пользователь клиент телеграмма
    /// </summary>
    public class TelegramClientUser : TelegramClient
    {
        public TelegramClientUser(ILogger logger, IConfiguration configuration, int apiId, string apiHash, string phoneNumber)
            : base(logger, configuration, apiId, apiHash)
        {
            _phoneNumber = phoneNumber;
        }

        #region Private Fields

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        private string _phoneNumber;

        /// <summary>
        /// Обработчик обновлений пользователя
        /// </summary>
        private UpdateHandlerUser _updater;

        #endregion

        #region Protected Methods

        protected override string GetTelegramPath(IConfiguration configuration)
        {
            var telegramPath = Path.Combine(configuration.GetValue<string>("TelegramPath"), _phoneNumber);
            return Environment.ExpandEnvironmentVariables(telegramPath);
        }

        #endregion

        #region Public Properties

        public override UpdateHandler Updater => _updater ??= new UpdateHandlerUser(this, Configuration);

        #endregion
    }
}
