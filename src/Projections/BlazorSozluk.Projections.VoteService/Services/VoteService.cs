using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
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
        await DeleteEntryVote(vote.entryId, vote.CreatedBy);

        using var connection = new SqlConnection(ConnectonString);

        await connection.ExecuteAsync("INSERT INTO EntryVote ( Id, CreatedAt, EntryId, VoteType, CreatedById)  " +
                                                     "VALUES(@Id, GETDATE(),@EntryId, @VoteType, @CreatedById)", 
            new
            {
                Id = Guid.NewGuid(),
                EntryId = vote.entryId,
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
    public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent vote)
    {
        await DeleteEntryCommentVote(vote.EntryCommentId, vote.CreatedById);

        using var connection = new SqlConnection(ConnectonString);

        await connection.ExecuteAsync("INSERT INTO EntryCommentVote ( Id, CreatedAt, EntryCommentId, VoteType, CreatedById)  " +
                                                     "VALUES(@Id, GETDATE(),@EntryCommentId, @VoteType, @CreatedById)",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = vote.EntryCommentId,
                VoteType = (int)vote.VoteType,
                CreatedById = vote.CreatedById
            });
    }


    public async Task DeleteEntryCommentVote(Guid EntryCommentId, Guid CreatedById)
    {
        using var connection = new SqlConnection(ConnectonString);

        await connection.ExecuteAsync("Delete From EntryCommentVote where EntryCommentId = @EntryCommentId AND CreatedById = @CreatedById",
            new
            {
                EntryCommentId = @EntryCommentId,
                CreatedById = @CreatedById
            });

    }



}
