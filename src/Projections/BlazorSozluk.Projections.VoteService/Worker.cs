using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Events.Entry;

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

        #region EntryCommentVote 

        QueueFactory.CreateBasiConsumer()
            .EnsurExchange(SozlukConstants.VoteExchangeName)
            .EnsureQueue(SozlukConstants.CreateEntryComemntVoteQueueName, SozlukConstants.VoteExchangeName)
            .Receive<CreateEntryCommentVoteEvent>(vote =>
            {
                // db insert
                voteService.CreateEntryCommentVote(vote).GetAwaiter().GetResult();
                _logger.LogInformation($"CreateEntryCommentVote Received EntryId {0}, VoteType: {1}", vote.EntryCommentId, vote.VoteType);
            })
            .StartConsuming(SozlukConstants.CreateEntryComemntVoteQueueName);



        QueueFactory.CreateBasiConsumer()
          .EnsurExchange(SozlukConstants.VoteExchangeName)
          .EnsureQueue(SozlukConstants.DeleteEntryCommentVoteQueueName, SozlukConstants.VoteExchangeName)
          .Receive<DeleteEntryCommentVoteEvent>(vote =>
          {
              // db insert
              voteService.DeleteEntryCommentVote(vote.EntryCommentId,vote.CreatedBy).GetAwaiter().GetResult();
              _logger.LogInformation($"DeleteEntryCommentVote Received EntryId {0}, VoteType: {1}", vote.EntryCommentId);
          })
          .StartConsuming(SozlukConstants.DeleteEntryCommentVoteQueueName);
        #endregion

        #region EntryVote 

        QueueFactory.CreateBasiConsumer()
            .EnsurExchange(SozlukConstants.VoteExchangeName)
            .EnsureQueue(SozlukConstants.CreateEntryVoteQueueName, SozlukConstants.VoteExchangeName)
            .Receive<CreateEntryVoteEvent>(vote =>
            {
               
                voteService.CreateEntryVote(vote).GetAwaiter().GetResult();
                _logger.LogInformation($"CreateEntryVote Received EntryId {0}, VoteType: {1}", vote.entryId, vote.VoteType);
            })
            .StartConsuming(SozlukConstants.CreateEntryVoteQueueName);



        QueueFactory.CreateBasiConsumer()
          .EnsurExchange(SozlukConstants.VoteExchangeName)
          .EnsureQueue(SozlukConstants.DeleteEntryVoteQueueName, SozlukConstants.VoteExchangeName)
          .Receive<DeleteEntryVoteEvent>(vote =>
          {
              
              voteService.DeleteEntryVote(vote.EntryId, vote.CreatedBy).GetAwaiter().GetResult();
              _logger.LogInformation($"DeleteEntryVote Received EntryId {0}, VoteType: {1}", vote.EntryId);
          })
          .StartConsuming(SozlukConstants.DeleteEntryVoteQueueName);


        #endregion
    }
}
