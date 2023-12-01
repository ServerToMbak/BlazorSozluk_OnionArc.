using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Common.Models.Queries;

public class BaseFooterRateViewModel
{
    public VoteType VoteType { get; set; }

}
public class BaseFooterFavoriteViewModel
{
    public bool IsFavorited { get; set; }
    public int FavoriteCount { get; set; }
}
public class BaseFooterRateFavoriteViewModel : BaseFooterFavoriteViewModel
{
    public VoteType VoteType { get; set; }
}
