using Microsoft.Extensions.Logging;
using Shared.Configuration;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
    public class Server
    {
        public Action OnServerStop;
        public Action<TcpClient> OnUserConnected;
        public Action<TcpClient> OnUserDisconnected;

        private TcpListener _listener;
        string _ip = "192.168.0.104";
        private int _port = 2812;
        private CancellationTokenSource _listenNewClientsCancellationTokenSource;
        private CancellationToken _listenNewClientsCancellationToken;
        private ILogger _logger;

        public Server(ILogger logger, MainConfiguration configuration)
        {
            _ip = configuration.GetServerSettings().Address;
            _port = configuration.GetServerSettings().Port;

            _listenNewClientsCancellationTokenSource = new CancellationTokenSource();
            _listenNewClientsCancellationToken = _listenNewClientsCancellationTokenSource.Token;
            _logger = logger;
        }

        public void Start(string ip, int port)
        {
            _ip = ip;
            _port = port;

            Start();
        }

        public void Start()
        {
            _listener = new TcpListener(IPAddress.Parse(_ip), _port);
            try
            {
                _listener.Start();
                _logger.LogInformation($"[Server] Server on {_listener.Server.LocalEndPoint} started");
                StartAcceptingNewConnections();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"[Server] Server exception: {ex}");
            }
        }

        public void Stop()
        {
            _listenNewClientsCancellationTokenSource.Cancel();
            _listener?.Stop();

            OnServerStop?.Invoke();

            _logger.LogInformation($"[Server] Server stopped");
        }

        private void StartAcceptingNewConnections()
        {
            Task.Run(() =>
            {
                while (!_listenNewClientsCancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation($"[Server] Accepting new clients...");
                        var client = _listener.AcceptTcpClient();
                        _logger.LogInformation($"[Server] Server accepted client : {client.Client.RemoteEndPoint}");

                        OnUserConnected?.Invoke(client);

                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical($"[Server] Server exception: {ex}");
                    }
                }
                _logger.LogInformation($"[Server] Server accepting new clients ended");
            }, _listenNewClientsCancellationToken);
        }
    }
}
