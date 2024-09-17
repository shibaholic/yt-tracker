using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnName("Password")
            .IsRequired();

        builder.Property(x => x.RefreshToken)
            .HasColumnName("RefreshToken");

        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .IsRequired()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}