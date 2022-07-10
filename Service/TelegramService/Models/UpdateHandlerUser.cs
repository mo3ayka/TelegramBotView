using TelegramService.Models.Updates;

namespace TelegramService.Models
{
    /// <summary>
    /// Обработчик обновлений пользователя
    /// </summary>
    public class UpdateHandlerUser : UpdateHandler<TelegramClientUser>
    {
        public UpdateHandlerUser(TelegramClientUser client, IConfiguration configuration)
            : base(client, configuration) { }

        protected override Dictionary<Type, IUpdateShell> FillProcessUpdates(TelegramClientUser client, IConfiguration configuration)
        {
            var updates = base.FillProcessUpdates(client, configuration);

            return updates;
        }
    }
}
