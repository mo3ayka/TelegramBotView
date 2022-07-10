using Telegram.Td.Api;

namespace TelegramService.Models.Updates
{
    /// <summary>
    /// Базовый обработчик обновления
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    public abstract class BaseUpdate<TUpdate, TClient> : IUpdateShell
        where TUpdate : Update
        where TClient : TelegramClient
    {
        public BaseUpdate(TClient client)
        {
            CurrentClient = client;
        }

        /// <summary>
        /// Родительский бот
        /// </summary>
        protected TClient CurrentClient;

        /// <summary>
        /// Основной обработчик обновлений
        /// </summary>
        /// <param name="update">Обрабатываемое обновление</param>
        protected abstract void ProcessUpdateBase(TUpdate update);

        public void ProcessUpdate(Update update)
        {
            ProcessUpdateBase((TUpdate)update);
        }
    }

    /// <summary>
    /// Оболочка над обновлением TDlib
    /// </summary>
    public interface IUpdateShell
    {
        /// <summary>
        /// Обработка обновления
        /// </summary>
        /// <param name="update"></param>
        void ProcessUpdate(Update update);
    }
}
