using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateVote;

public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
                                           exchangeType: SozlukConstants.DefaultExchangeType,
                                           queueName: SozlukConstants.CreateEntryComemntVoteQueueName,
                                           obj: new CreateEntryCommentVoteEvent() 
                                           {
                                                CreatedBy = request.CreatedBy,  
                                                EntryCommentId = request.EntryCommentId,
                                                VoteType = request.VoteType,
                                           });

        return await Task.FromResult(true); 
    }
}
