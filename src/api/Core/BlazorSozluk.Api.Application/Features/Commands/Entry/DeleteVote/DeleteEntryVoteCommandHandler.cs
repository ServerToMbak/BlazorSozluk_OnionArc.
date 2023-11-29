using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;

public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
                                            exchangeType: SozlukConstants.DefaultExchangeType,
                                            queueName: SozlukConstants.DeleteEntryVoteQueueName,
                                            obj: new DeleteEntryVoteEvent() 
                                            {
                                                CreatedBy = request.UserId,
                                                EntryId = request.EntryId,
                                            });

        return await Task.FromResult(true); 
    }
}
