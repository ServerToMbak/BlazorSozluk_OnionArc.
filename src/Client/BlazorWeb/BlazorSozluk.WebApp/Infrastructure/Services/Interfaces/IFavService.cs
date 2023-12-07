namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IFavService
    {
        Task CreatedEntryComemntFav(Guid entryCommentId);
        Task CreatedEntryFav(Guid? entryId);
        Task DeleteEntryCommentFav(Guid entryCommentId);
        Task DeleteEntryFav(Guid? entryCommentId);
    }
}