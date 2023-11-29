using BlazorSozluk.Common.ViewModels;
using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class CreateEntryVoteCommand : IRequest<bool> 
{
    public Guid EntryId { get; set; }
    public VoteType  VoteType { get; set; }
    public Guid CreatedBy { get; set; }
}
