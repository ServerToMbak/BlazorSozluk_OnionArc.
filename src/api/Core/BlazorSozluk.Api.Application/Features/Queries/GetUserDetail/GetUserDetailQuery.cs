using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserDetail;

public class GetUserDetailQuery : IRequest<UserDetailViewModel>
{
    public GetUserDetailQuery(Guid userId, string userName = null)
    {
        UserId = userId;
        UserName = userName; 
    }

    public Guid UserId { get; set; }
    public string UserName { get; set; } // we will be able to search user by user name too thats why we
                                         // have added UserName query string to hear
    
}
