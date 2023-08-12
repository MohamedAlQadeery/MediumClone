using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.ArticleTagEntity;
using MediumClone.Domain.FollowingEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MediumClone.Infrastructure.Persistence.Configurations;

public class FollowingsConfiguration : IEntityTypeConfiguration<Following>
{

    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder.HasOne(x => x.FollowingUser).WithMany(x => x.Followings)
        .HasForeignKey(x => x.FollowingUserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.FollowedUser).WithMany(x => x.Followers)
        .HasForeignKey(x => x.FollowedUserId).OnDelete(DeleteBehavior.NoAction);


    }
}


