using Telegram.Td.Api;
using TelegramService.Models.Updates;

namespace TelegramService.Models
{
    public abstract class UpdateHandler<TClient> : IUpdateHandler
        where TClient : TelegramClient
    {
        public UpdateHandler(TClient client, IConfiguration configuration)
        {
            _processUpdates = FillProcessUpdates(client, configuration);
        }

        /// <summary>
        /// Доступные реализации обработки обновлений
        /// </summary>
        private Dictionary<Type, IUpdateShell> _processUpdates;

        /// <summary>
        /// Заполнить реализации обновлений
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual Dictionary<Type, IUpdateShell> FillProcessUpdates(TClient client, IConfiguration configuration)
        {
            return new Dictionary<Type, IUpdateShell>();
        }

        /// <summary>
        /// Вызов обработчика обновления
        /// </summary>
        /// <param name="update"></param>
        public void InvokeProcessUpdate(Update update)
        {
            var updateType = update.GetType();
            if (_processUpdates.ContainsKey(updateType))
                _processUpdates[updateType].ProcessUpdate(update);
        }
    }

    /// <summary>
    /// Обработчик обновлений
    /// </summary>
    public interface IUpdateHandler
    {
        /// <summary>
        /// Обработка обновления
        /// </summary>
        /// <param name="update"></param>
        void InvokeProcessUpdate(Update update);
    }
}
