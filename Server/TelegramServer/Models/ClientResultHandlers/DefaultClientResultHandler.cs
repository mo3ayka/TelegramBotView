using Telegram.Td;
using Telegram.Td.Api;

namespace TelegramServer.Models.ClientResultHandlers
{
    /// <summary>
    /// Базовый обработчик получения результатов
    /// </summary>
    public class DefaultClientResultHandler : ClientResultHandler
    {
        public DefaultClientResultHandler(TelegramClient parentClient)
        {
            _parentClient = parentClient;
        }

        /// <summary>
        /// Родительский бот
        /// </summary>
        private TelegramClient _parentClient;

        public void OnResult(BaseObject @object)
        {
            switch (@object)
            {
                case Error error:
                    {
                        _parentClient.Logger.LogError($"{error.Code} : {error.Message}");
                        break;
                    }
                case Update update:
                    {
                        break;
                    }
            }
        }
    }
}
