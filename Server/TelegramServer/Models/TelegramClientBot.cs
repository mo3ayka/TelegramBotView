using Telegram.Td;
using Telegram.Td.Api;

namespace TelegramServer.Models
{
    /// <summary>
    /// Бот клиент телеграмма
    /// </summary>
    public class TelegramClientBot : TelegramClient
    {
        public TelegramClientBot(ILogger logger, IConfiguration configuration, int apiId, string apiHash, string botToken)
            : base(logger, configuration, apiId, apiHash)
        {
            _botToken = botToken;
        }

        #region Private Fields

        /// <summary>
        /// Токен бота
        /// </summary>
        private string _botToken;

        /// <summary>
        /// Обработчик обновлений бота
        /// </summary>
        private UpdateHandlerBot _updater;

        #endregion

        #region Protected Methods

        protected override string GetTelegramPath(IConfiguration configuration)
        {
            var telegramPath = Path.Combine(configuration.GetValue<string>("TelegramPath"), _botToken.Replace(":", "_"));
            return Environment.ExpandEnvironmentVariables(telegramPath);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Токен бота
        /// </summary>
        public string BotToken => _botToken;
        
        public override UpdateHandler Updater => _updater ??= new UpdateHandlerBot(this, Configuration);

        #endregion
    }
}
