using Telegram.Td.Api;
using TelegramService.Models.Updates;

namespace TelegramService.Models
{
    /// <summary>
    /// Обработчик обновлений бота
    /// </summary>
    public class UpdateHandlerBot : UpdateHandler<TelegramClientBot>
    {
        public UpdateHandlerBot(TelegramClientBot client, IConfiguration configuration)
            : base(client, configuration) { }

        protected override Dictionary<Type, IUpdateShell> FillProcessUpdates(TelegramClientBot client, IConfiguration configuration)
        {
            var updates = base.FillProcessUpdates(client, configuration);

            updates.TryAdd(typeof(UpdateAuthorizationState), new BotAuthorizationStateUpdate(client));

            return updates;
        }
    }
}
