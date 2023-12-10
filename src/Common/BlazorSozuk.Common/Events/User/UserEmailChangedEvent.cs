namespace BlazorSozluk.Common.Events.User;

public class UserEmailChangedEvent
{
    public string NewEmailAddress { get; set; }
    public string OldEmailAddress { get; set; }
}
