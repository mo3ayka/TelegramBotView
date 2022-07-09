using Telegram.Td.Api;

namespace TelegramServer.Models.Updates
{
    public class BotAuthorizationStateUpdate : BaseUpdate<UpdateAuthorizationState, TelegramClientBot>
    {
        public BotAuthorizationStateUpdate(TelegramClientBot client) : base(client) { }

        protected override void ProcessUpdateBase(UpdateAuthorizationState update)
        {
            switch (update.AuthorizationState)
            {
                case AuthorizationStateWaitTdlibParameters:
                    {
                        var parameters = new TdlibParameters
                        {
                            DatabaseDirectory = CurrentClient.ClientDirectory,
                            UseMessageDatabase = true,
                            UseChatInfoDatabase = true,
                            UseSecretChats = true,
                            ApiId = CurrentClient.ApiId,
                            ApiHash = CurrentClient.ApiHash,
                            SystemLanguageCode = "ru",
                            DeviceModel = "Desktop",
                            ApplicationVersion = "2.0",
                            EnableStorageOptimizer = true
                        };
                        CurrentClient.SendFunction(new SetTdlibParameters(parameters));
                        break;
                    }
                case AuthorizationStateWaitEncryptionKey:
                    {
                        CurrentClient.SendFunction(new CheckDatabaseEncryptionKey());
                        break;
                    }
                case AuthorizationStateWaitPhoneNumber:
                    {
                        CurrentClient.SendFunction(new CheckAuthenticationBotToken(CurrentClient.BotToken));
                        break;
                    }
                case AuthorizationStateReady:
                    {
                        CurrentClient.SendFunction(new GetMe());
                        break;
                    }
            }
        }
    }
}
