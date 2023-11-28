using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Update;

public class UpdateUserCommndHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommndHandler(IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetByIdAsync(request.Id);
       
        if (dbUser is null)
            throw new DatabaseValidationException("User not found!");


        var dbEmailAddress = dbUser.EmailAddress;
        var EmailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;


        _mapper.Map(request, dbUser);

        var rows = await _userRepository.UpdateAsync(dbUser);

        //Check if email changed
        if (EmailChanged && rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = dbEmailAddress,
                NewEmailAddress = dbUser.EmailAddress,
            };

            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                               exchangeType: SozlukConstants.DefaultExchangeType,
                                               queueName: SozlukConstants.UserEmailChangedQueuName,
                                               obj: @event);
            dbUser.EmailConfirmed = false;
            await _userRepository.UpdateAsync(dbUser);

        }
        return dbUser.Id;
    }
}
