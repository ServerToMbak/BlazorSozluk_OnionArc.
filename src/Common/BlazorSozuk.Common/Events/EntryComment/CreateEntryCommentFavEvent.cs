namespace BlazorSozluk.Common.Events.EntryComment;

public class CreateEntryCommentFavEvent
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedById { get; set; }
}
