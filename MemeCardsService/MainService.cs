using Networking;

namespace MemeCardsService
{
    public class MainService : BackgroundService
    {
        private readonly ILogger<MainService> _logger;
        private readonly Server _server;

        public MainService(ILogger<MainService> logger, Server server)
        {
            _logger = logger;
            _server = server;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(" ================ Server starting ================ ");
            _server.Start();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(5000, stoppingToken);
            }

            _server.Stop();
            _logger.LogInformation(" ================ Server stopping ================ ");

        }
    }
}
