using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Models;
using Dapper;
using System.Data.SqlClient;

namespace BlazorSozluk.Projections.VoteService.Services;

public class VoteService
{
    private readonly string ConnectonString;

    public VoteService(string connectonString)
    {
        ConnectonString = connectonString;
    }

    public async Task CreateEntryVote(CreateEntryVoteEvent vote)
    {
        await DeleteEntryVote(vote.EntryId, vote.CreatedBy);

        using var connection = new SqlConnection(ConnectonString);

        await connection.ExecuteAsync("INSERT INTO ENTRYVOTE (Id, CretedAt, EntryId, VoteType, CreatedById)  VALUES(@Id, GETDATE(),@EntryId, @VoteType, @CreatedBy)", new
        {
            Id = Guid.NewGuid(),
            EntryId = vote.EntryId,
            VoteType = (int)vote.VoteType,
            CreatedById = vote.CreatedBy
        });
    }


    public async Task DeleteEntryVote(Guid entryId, Guid userId)
    {
        using var connection = new SqlConnection(ConnectonString);

        await connection.ExecuteAsync("Delete From EntryVote where EntryId = @entryId AND CreatedById = @userId",
            new
            {
                EntryId = entryId,
                UserId = userId 
            });

    }
}
