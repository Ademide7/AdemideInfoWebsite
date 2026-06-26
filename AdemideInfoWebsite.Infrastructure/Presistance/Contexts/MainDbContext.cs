using AdemideInfoWebsite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; 
using System.Text;

namespace AdemideInfoWebsite.Infrastructure.Presistance.Contexts;

//MainDbContext class that inherits from DbContext using Microsoft.EntityFrameworkCore. It has a constructor that takes DbContextOptions<MainDbContext> as a parameter and passes it to the base class constructor. It also has DbSet properties for Profile, Password, Appointment, and UserActivity entities. The OnModelCreating method is overridden to configure the entity properties and relationships using the Fluent API.
public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Password> Passwords { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Language).IsRequired();
            entity.HasMany(e => e.Appointments).WithOne().HasForeignKey("ProfileId");
            entity.HasMany(e => e.UserActivities).WithOne().HasForeignKey("ProfileId");
        });

        modelBuilder.Entity<Password>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.HashedPassword).IsRequired();
        }); 

        modelBuilder.Entity<Appointment>()
    .HasOne(a => a.Profile)
    .WithMany(p => p.Appointments)
    .HasForeignKey(a => a.ProfileId)
    .OnDelete(DeleteBehavior.NoAction);

        // or NoAction
        modelBuilder.Entity<UserActivity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(u => u.Profile)
                  .WithMany(p => p.UserActivities)
                  .HasForeignKey(u => u.ProfileId)
                  .OnDelete(DeleteBehavior.NoAction); // or Restrict
        });
    }
}