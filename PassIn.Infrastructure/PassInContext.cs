using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;

namespace PassIn.Infrastructure;

public class PassInContext : DbContext
{
  public PassInContext(DbContextOptions<PassInContext> options) : base(options) { }

  public DbSet<Attendees> Attendees { get; set; }
  public DbSet<Event> Events { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Attendees>().HasKey(at => at.Id);
    modelBuilder.Entity<Attendees>().Property(at => at.Id).ValueGeneratedOnAdd();

    modelBuilder.Entity<Event>().HasKey(ev => ev.Id);
    modelBuilder.Entity<Event>().Property(ev => ev.Id).ValueGeneratedOnAdd();
  }

}
