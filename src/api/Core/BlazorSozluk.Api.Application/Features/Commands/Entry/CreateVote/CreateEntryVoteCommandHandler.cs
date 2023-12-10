using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateVote;

public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
{

    public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
                                          exchangeType: SozlukConstants.DefaultExchangeType,
                                          queueName: SozlukConstants.CreateEntryVoteQueueName,
                                          obj: new CreateEntryVoteEvent() 
                                          {
                                            CreatedBy = request.CreatedBy,
                                            entryId = request.EntryId,
                                            VoteType = request.VoteType,
                                          });

        return await Task.FromResult(true); 
    }
}
