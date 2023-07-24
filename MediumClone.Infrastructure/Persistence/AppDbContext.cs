using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.ProductCategoryEntity;
using MediumClone.Domain.TagEntity;
using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.ArticleTagEntity;

namespace MediumClone.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<ArticleTag> ArticleTags { get; set; } = null!;





}