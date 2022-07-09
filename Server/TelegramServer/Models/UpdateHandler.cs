using Telegram.Td.Api;

namespace TelegramServer.Models
{
    public abstract class UpdateHandler<TClient>
        where TClient : TelegramClient
    {
        public UpdateHandler(TClient client, IConfiguration configuration)
        {
            _processUpdates = FillProcessUpdates(client, configuration);
        }

        /// <summary>
        /// Доступные реализации обработки обновлений
        /// </summary>
        private Dictionary<Type, IProcessUpdate> _processUpdates;

        /// <summary>
        /// Заполнить реализации обновлений
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual Dictionary<Type, IProcessUpdate> FillProcessUpdates(TClient client, IConfiguration configuration)
        {
            return new Dictionary<Type, IProcessUpdate>();
        }

        /// <summary>
        /// Обработать обновление
        /// </summary>
        /// <param name="update"></param>
        public void ProcessUpdate(Update update)
        {
            var updateType = update.GetType();
            if (_processUpdates.ContainsKey(updateType))
                _processUpdates[updateType].ProcessUpdate(update);
        }
    }

    /// <summary>
    /// Обработчик обновлений
    /// </summary>
    public interface IProcessUpdate
    {
        /// <summary>
        /// Обработка обновления
        /// </summary>
        /// <param name="update"></param>
        void ProcessUpdate(Update update);
    }
}
