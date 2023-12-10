using BlazorSozluk.Common.Models;

namespace BlazorSozluk.Common.Events.EntryComment;

public class CreateEntryCommentVoteEvent
{
    public Guid EntryCommentId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedById { get; set; }
}
