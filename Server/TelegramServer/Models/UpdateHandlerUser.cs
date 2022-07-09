namespace TelegramServer.Models
{
    public class UpdateHandlerUser : UpdateHandler<TelegramClientUser>
    {
        public UpdateHandlerUser(TelegramClientUser client, IConfiguration configuration)
            : base(client, configuration) { }

        protected override Dictionary<Type, IProcessUpdate> FillProcessUpdates(TelegramClientUser client, IConfiguration configuration)
        {
            var updates = base.FillProcessUpdates(client, configuration);

            return updates;
        }
    }
}
