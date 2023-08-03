using MediumClone.Domain.TagEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediumClone.Infrastructure.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        // builder.HasMany(x => x.ArticleTags).WithOne(x => x.Tag).HasForeignKey(x => x.TagId);

    }
}


