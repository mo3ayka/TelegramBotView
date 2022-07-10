using Grpc.Core;
using TelegramService;

namespace TelegramService.Services
{
    public class MainService : Main.MainBase
    {
        private readonly ILogger<MainService> _logger;
        public MainService(ILogger<MainService> logger)
        {
            _logger = logger;
        }

        public override Task<PingReply> Ping(PingRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PingReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}