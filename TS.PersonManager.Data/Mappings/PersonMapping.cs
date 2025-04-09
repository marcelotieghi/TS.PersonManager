using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TS.PersonManager.Core.Entities;

namespace TS.PersonManager.Data.Mappings;

internal class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons");

        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.FirstName)
            .HasColumnName("FirstName")
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(p => p.LastName)
            .HasColumnName("LastName")
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("nvarchar")
            .HasMaxLength(256)
            .IsRequired()
            .IsUnicode(true);

        builder
             .HasIndex(p => p.Email)
             .IsUnique();

        builder
            .Property(p => p.PhoneNumber)
            .HasColumnName("PhoneNumber")
            .HasColumnType("nvarchar")
            .HasMaxLength(15)
            .IsRequired()
            .IsUnicode(false);

        builder
            .Property(p => p.DateOfBirth)
            .HasColumnName("DateOfBirth")
            .HasColumnType("date")
            .IsRequired();

        builder
            .Property(p => p.Gender)
            .HasColumnName("Gender")
            .HasColumnType("nvarchar")
            .HasMaxLength(10)
            .IsRequired(false);

        builder
            .Property(p => p.ImagePath)
            .HasColumnName("ImagePath")
            .HasColumnType("nvarchar")
            .HasMaxLength(255)
            .IsRequired();
    }
}