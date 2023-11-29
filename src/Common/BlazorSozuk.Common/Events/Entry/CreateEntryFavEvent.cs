namespace BlazorSozluk.Common.Events.Entry;

public class CreateEntryFavEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedById { get; set; }
}
