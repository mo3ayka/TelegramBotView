using Telegram.Td;
using Telegram.Td.Api;

namespace TelegramService.Models.ClientResultHandlers
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
        /// Родительский клиент
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
                        _parentClient.Updater.InvokeProcessUpdate(update);
                        break;
                    }
            }
        }
    }
}
