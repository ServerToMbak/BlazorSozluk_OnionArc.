using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
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
                    "VALUES(@Id, @EntryId, @CreatedById, GETDATE())",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = @event.EntryId,
                CreatedById =  @event.CreatedById,
            });
    }
    public async Task CreateEntryCommentFav(CreateEntryCommentFavEvent @event)
    {

        using var connection = new SqlConnection(ConnectionString);

        await connection.ExecuteAsync
            ("Insert Into EntryCommentFavorite(Id, EntryCommentId, CreatedById, CreatedAt) " +
                                      "VALUES(@Id,@EntryCommentId,@CreatedById, GETDATE())",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = @event.EntryCommentId,
                CreatedById = @event.CreatedById,
            });
    }

    public async Task DeleteEntryFav(DeleteEntryFavEvent @event)
    {

        using var connection = new SqlConnection(ConnectionString);

        await connection.ExecuteAsync
        ("Delete FROM EntryFavorite Where EntryId = @EntryId AND CreatedById = @CreatedById",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = @event.EntryId,
                CreatedById = @event.CreatedBy,
            });
    }

    public async Task DeleteEntryCommentFav(DeleteEntryCommentFavEvent @event)
    {

        using var connection = new SqlConnection(ConnectionString);

        await connection.ExecuteAsync
        ("Delete FROM EntryCommentFavorite Where EntryCommentId = @EntryCommentId AND CreatedById = @CreatedById",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = @event.EntryCommentId,
                CreatedById = @event.CreatedById,
            });
    }
}
