namespace BlazorSozluk.Common;

public class SozlukConstants
{

#if DEBUG
    public const string RabbitMqHost = "localhost";
#else
    public const string RabbitMqHost = "rabbit-server";
#endif
    public const string DefaultExchangeType = "direct";


    public const string UserExchangeName = "UserExchange";
    public const string UserEmailChangedQueuName = "UserEmailChangedQueue";


    //Fav
    public const string FavExchangeName = "FavExchangeName";
    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
    public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
    public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
    public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";

    
    //Vote
    public const string VoteExchangeName = "VoteExchangeName";
    public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
    public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";
    public const string CreateEntryComemntVoteQueueName = "CreateEntryComemntVoteQueue";
    public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";



}
