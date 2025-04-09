using Microsoft.EntityFrameworkCore;
using TS.PersonManager.Core.Entities;

namespace TS.PersonManager.Data.Context;

public sealed class PersonContext(DbContextOptions<PersonContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonContext).Assembly);
    }
}