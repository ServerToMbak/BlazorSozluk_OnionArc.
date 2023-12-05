using System.ComponentModel.DataAnnotations;

namespace BlazorSozluk.Api.Domain.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
