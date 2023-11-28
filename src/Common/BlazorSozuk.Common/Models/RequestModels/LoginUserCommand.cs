using BlazorSozluk.Common.Models.Queries;
using MediatR;
namespace BlazorSozluk.Common.Models.RequestModels;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public string EmailAddress { get;  set; }
    public string Password { get;  set; }
    public LoginUserCommand(string emailAddres, string password)
    {
        EmailAddress = emailAddres;
        Password = password;
    }
    public LoginUserCommand()
    {
        
    }
}
