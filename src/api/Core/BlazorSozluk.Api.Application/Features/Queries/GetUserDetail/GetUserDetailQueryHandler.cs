using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserDetail;

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserDetailQueryHandler(IUserRepository userRepository , IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        User dbUser = null;

        if (request.UserId != Guid.Empty)
            dbUser = await _userRepository.GetByIdAsync(request.UserId);
        else if (!string.IsNullOrEmpty(request.UserName))
            dbUser = await _userRepository.GetSingleAsync(i => i.UserName == request.UserName);

        //TODO if both are empthy, throw new exception

        return _mapper.Map<UserDetailViewModel>(dbUser);

    }
}
