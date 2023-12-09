using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;

namespace BlazorSozluk.Projections.VoteService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger , IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var connStr = _configuration.GetConnectionString("SqlServer");

        var voteService = new Services.VoteService(connStr);

        QueueFactory.CreateBasiConsumer()
            .EnsurExchange(SozlukConstants.FavExchangeName)
            .EnsurQueue(SozlukConstants.CreateEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
            .Receive<CreateEntryVoteEvent>(vote =>
            {
                // db insert
                voteService.CreateEntryVote(vote).GetAwaiter().GetResult();
                _logger.LogInformation($"Received Entry Received EntryId {0}, VoteType: {1}", vote.EntryId, vote.VoteType);
            })
            .StartConsuming(SozlukConstants.CreateEntryVoteQueueName);
    }
}
