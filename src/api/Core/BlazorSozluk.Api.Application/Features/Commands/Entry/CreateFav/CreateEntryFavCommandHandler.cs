using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav;

public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{
    public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
                                        exchangeType: SozlukConstants.DefaultExchangeType,
                                        queueName: SozlukConstants.CreateEntryFavQueueName,
                                        obj: new CreateEntryFavEvent()
                                        { 
                                            EntryId =  request.EntryId.Value,
                                            CreatedById = request.UserId.Value
                                        });

        return Task.FromResult(true);
    }
}
