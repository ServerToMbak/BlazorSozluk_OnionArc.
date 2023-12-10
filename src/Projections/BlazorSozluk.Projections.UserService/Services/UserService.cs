using BlazorSozluk.Common.Events.User;
using Dapper;
using System.Data.SqlClient;

namespace BlazorSozluk.Projections.UserService.Services;

public class UserService
{

    private string ConnStr;

    public UserService(IConfiguration configuration)
    {
        ConnStr = configuration.GetConnectionString("SqlServer");
    }

    public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
    {
        var guid = Guid.NewGuid();

        using var connection = new SqlConnection(ConnStr);
        await connection.ExecuteAsync("INSERT INTO EMAILCONFIRMATION (Id, CreatedAt, OldEmailAddress,NewEmailAddress)" +
                                                           "VALUES (@Id, GETDATE(), @OldEmailAddress, @NewEmailAddress)",
        new
        {
            Id = guid,
            OldEmailAddress = @event.OldEmailAddress,
            NewEmailAddress = @event.NewEmailAddress,

        });

        return guid;
    }
}
