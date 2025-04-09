using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TS.PersonManager.Core.Entities;

namespace TS.PersonManager.Data.Mappings;

internal class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(e => e.Id);

        builder
            .Property(t => t.UserName)
            .HasColumnName("UserName")
            .IsRequired();

        builder
            .Property(t => t.Password)
            .HasColumnName("Password")
            .IsRequired();
    }
}