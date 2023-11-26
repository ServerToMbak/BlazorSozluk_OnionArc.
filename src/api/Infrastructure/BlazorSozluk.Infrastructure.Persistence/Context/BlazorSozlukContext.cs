using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlazorSozluk.Api.Infrastructure.Persistence.Context;

public class BlazorSozlukContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";
    public BlazorSozlukContext()
    {
        
    }
    public BlazorSozlukContext(DbContextOptions options): base(options) 
    {    
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Entry> Entries { get; set; }

    public DbSet<EntryVote> EntryVotes { get; set; }
    public DbSet<EntryFavorite> EntryFavorites { get; set; }

    public DbSet<EntryComment> EntryComments { get; set; }
    public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
    public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }


    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var connStr = "server=(localdb)\\MSSQLLocalDB; database=BlazorSozluk ;trusted_connection=true";
            optionsBuilder.UseSqlServer(connStr,opt=>
            {
                opt.EnableRetryOnFailure();
            });
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries()
                                            .Where(i => i.State == EntityState.Added)
                                            .Select(i => (BaseEntity)i.Entity);
        PreparedAddedEntites(addedEntities);
    }

    private void PreparedAddedEntites(IEnumerable<BaseEntity> entites)
    {
        foreach (var entity in entites)
        {
            if(entity.CreatedAt == DateTime.MinValue)
                entity.CreatedAt = DateTime.Now;
        }
    }
}
