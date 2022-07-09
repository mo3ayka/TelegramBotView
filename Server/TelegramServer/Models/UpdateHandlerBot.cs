using Telegram.Td.Api;
using TelegramServer.Models.Updates;

namespace TelegramServer.Models
{
    public class UpdateHandlerBot : UpdateHandler<TelegramClientBot>
    {
        public UpdateHandlerBot(TelegramClientBot client, IConfiguration configuration)
            : base(client, configuration) { }

        protected override Dictionary<Type, IProcessUpdate> FillProcessUpdates(TelegramClientBot client, IConfiguration configuration)
        {
            var updates = base.FillProcessUpdates(client, configuration);

            updates.TryAdd(typeof(UpdateAuthorizationState), new BotAuthorizationStateUpdate(client));

            return updates;
        }
    }
}
