using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace TelegramServiceConnector.Services
{
    public class MainService : Main.MainClient
    {
        #region Private Fields

        /// <summary>
        /// Адрес сервера
        /// </summary>
        private string _serverAddress;

        /// <summary>
        /// Порт сервера
        /// </summary>
        private string _serverPort;

        /// <summary>
        /// Канал подключения к серверу
        /// </summary>
        private Channel _channel;

        /// <summary>
        /// Вызов на сервер
        /// </summary>
        private CallInvoker _callInvoker;

        /// <summary>
        /// Объект основного сервиса сервера
        /// </summary>
        private Main.MainClient _mainClient;

        #endregion Private Fields

        public MainService(string serverAddress, string serverPort)
        {
            _serverAddress = serverAddress;
            _serverPort = serverPort;
            _channel = new Channel($"{_serverAddress}:{_serverPort}", ChannelCredentials.Insecure);

            _callInvoker = _channel.CreateCallInvoker();
            _mainClient = new Main.MainClient(_callInvoker);
        }
    }
}
