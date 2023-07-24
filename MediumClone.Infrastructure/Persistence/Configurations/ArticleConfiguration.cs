using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.ArticleTagEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MediumClone.Infrastructure.Persistence.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Body).IsRequired();
        builder.HasOne(x => x.Author).WithMany(x => x.Articles).HasForeignKey(x => x.AuthorId);
        builder.HasMany(x => x.ArticleTags).WithOne(x => x.Article).HasForeignKey(x => x.ArticleId);
        builder.Navigation(x => x.ArticleTags).AutoInclude();


    }
}

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.HasKey(x => new { x.ArticleId, x.TagId });
        builder.HasOne(x => x.Article).WithMany(x => x.ArticleTags).HasForeignKey(x => x.ArticleId);
        builder.HasOne(x => x.Tag).WithMany(x => x.ArticleTags).HasForeignKey(x => x.TagId);



    }
}

