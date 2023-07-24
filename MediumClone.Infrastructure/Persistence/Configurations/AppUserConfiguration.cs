using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.AppUserEntity.Enums;

namespace MediumClone.Infrastructure.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.OwnsOne(au => au.Address);

        builder.Property(au => au.AppUserRole)
                .HasConversion(role => role.Value, roleValue => AppUserRole.FromValue(roleValue));

        builder.HasMany(au => au.Articles).WithOne(a => a.Author).HasForeignKey(a => a.AuthorId);
    }
}
