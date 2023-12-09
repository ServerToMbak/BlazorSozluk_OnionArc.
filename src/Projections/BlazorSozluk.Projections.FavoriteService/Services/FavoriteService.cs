using BlazorSozluk.Common.Events.Entry;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BlazorSozluk.Projections.FavoriteService.Services;

public class FavoriteService
{
    private readonly string ConnectionString;

    public FavoriteService(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public async Task CreateEntryFav(CreateEntryFavEvent @event)
    {

        using var connection = new SqlConnection(ConnectionString);

        await connection.ExecuteAsync
            ("Insert Into EntryFavorite(Id, EntryId, CreatedById, CreatedAt) " +
                    "VALUES(@Id, @EntryId, @CreatedBy, GETDATE())",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = @event.EntryId,
                CreatedById =  @event.CreatedById,
            });
    }
}
