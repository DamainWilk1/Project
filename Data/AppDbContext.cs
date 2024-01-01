using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ContactEntity> Contacts { get; set; } 
        public DbSet<OrganizationEntity> Organizations { get; set; }
        private string Path { get; set; }  

        public AppDbContext() 
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            Path = System.IO.Path.Join(path, "contacts.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"data source={Path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                     
            base.OnModelCreating(modelBuilder);

            // Tworzenie użytkownika
            var user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test",
                NormalizedUserName = "TEST",
                Email = "test@wsei.edu.pl",
                NormalizedEmail = "TEST@WSEI.EDU.PL",
                EmailConfirmed = true
            };

            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "1234Ab!");

            modelBuilder.Entity<IdentityUser>()
                .HasData(user);

            // Tworzenie Roli
            var adminRule = new IdentityRole()
            {
                Id = "ADMIN_ID",
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            adminRule.ConcurrencyStamp = adminRule.Id;

            // Nadanie użytkownikowi roli
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>()
                    {
                        RoleId = adminRule.Id,
                        UserId = user.Id,
                    }
                );

            modelBuilder.Entity<IdentityRole>()
                .HasData(adminRule);

            modelBuilder.Entity<ContactEntity>()
                .HasOne(e => e.Organization)
                .WithMany(o => o.Contacts)
                .HasForeignKey(e => e.OrganizationId);
            modelBuilder.Entity<OrganizationEntity>()
                .HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    Name = "WSEI",
                    Description = "Uczelnia Wyższa"
                },
                new OrganizationEntity()
                {
                    Id = 102,
                    Name = "Koło studenckie VR",
                    Description = "Uczelnia Wyższa"
                }
                );

            modelBuilder.Entity<ContactEntity>().HasData(
                new ContactEntity() 
                { 
                    Id = 1, 
                    Name = "Adam", 
                    Email = "Adam@wsei.edu.pl", 
                    Phone = "123456",
                    OrganizationId = 102
                },
                new ContactEntity() 
                { 
                    Id = 2, 
                    Name = "Czesio", 
                    Email = "Czesio@wsei.edu.pl", 
                    Phone = "6315553",
                    OrganizationId = 101
                },
                new ContactEntity() 
                { 
                    Id = 3, 
                    Name = "Karolina",
                    Email = "Karolina@wsei.edu.pl", 
                    Phone = "43214321" 
                }
                );
            modelBuilder.Entity<OrganizationEntity>()
                .OwnsOne(o => o.Address)
                .HasData(
                new
                {
                    OrganizationEntityId = 101,
                    City = "Kraków",
                    Street = "Św. Filipa 17",
                    PostalCode = "31-150"
                },
                new
                {
                    OrganizationEntityId = 102,
                    City = "Kraków",
                    Street = "Św. Filipa 17, pok. 12",
                    PostalCode = "31-150"
                }
                );
        }
    }
}
