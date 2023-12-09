using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Projections.FavoriteService.Services;

namespace BlazorSozluk.Projections.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _Configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = _Configuration.GetConnectionString("SqlServer");
            var favService = new Services.FavoriteService(connStr);

            QueueFactory.CreateBasiConsumer()
                .EnsurExchange(SozlukConstants.FavExchangeName)
                .EnsurQueue(SozlukConstants.CreateEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
                .Receive<CreateEntryFavEvent>(fav =>
                {
                    // db insert
                    favService.CreateEntryFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Received EntryId {fav.EntryId}");
                })
                .StartConsuming(SozlukConstants.CreateEntryFavQueueName);
        }
    }
}
