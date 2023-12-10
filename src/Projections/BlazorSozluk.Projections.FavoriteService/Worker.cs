using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;

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

            #region Entry
            QueueFactory.CreateBasiConsumer()
                .EnsurExchange(SozlukConstants.FavExchangeName)
                .EnsureQueue(SozlukConstants.CreateEntryFavQueueName, SozlukConstants.FavExchangeName)
                .Receive<CreateEntryFavEvent>(fav =>
                {
                    // db insert
                    favService.CreateEntryFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Received CreateEntryFavEvent EntryId {fav.EntryId}");
                })
                .StartConsuming(SozlukConstants.CreateEntryFavQueueName);

            QueueFactory.CreateBasiConsumer()
              .EnsurExchange(SozlukConstants.FavExchangeName)
              .EnsureQueue(SozlukConstants.DeleteEntryFavQueueName, SozlukConstants.FavExchangeName)
              .Receive<DeleteEntryFavEvent>(fav =>
              {
                  // db insert
                  favService.DeleteEntryFav(fav).GetAwaiter().GetResult();
                  _logger.LogInformation($"Received DeleteEntryFavEvent EntryId {fav.EntryId}");
              })
              .StartConsuming(SozlukConstants.DeleteEntryFavQueueName);

            #endregion


            #region EntryComment

            QueueFactory.CreateBasiConsumer()
               .EnsurExchange(SozlukConstants.FavExchangeName)
               .EnsureQueue(SozlukConstants.CreateEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
               .Receive<CreateEntryCommentFavEvent>(fav =>
               {
                   // db insert
                   favService.CreateEntryCommentFav(fav).GetAwaiter().GetResult();
                   _logger.LogInformation($"Received CreaeteEntryCommentFav EntryCommentId {fav.EntryCommentId}");
               })
               .StartConsuming(SozlukConstants.CreateEntryCommentFavQueueName);




            QueueFactory.CreateBasiConsumer()
              .EnsurExchange(SozlukConstants.FavExchangeName)
              .EnsureQueue(SozlukConstants.DeleteEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
              .Receive<DeleteEntryCommentFavEvent>(fav =>
              {
                  // db insert
                  favService.DeleteEntryCommentFav(fav).GetAwaiter().GetResult();
                  _logger.LogInformation($"DeleteEntryCommentFav Received EntryCommentId {fav.EntryCommentId}");
              })
              .StartConsuming(SozlukConstants.DeleteEntryCommentFavQueueName);

            #endregion
        }
    }
}
