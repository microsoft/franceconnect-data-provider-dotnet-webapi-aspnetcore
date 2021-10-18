// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApi_Data_Provider_DotNet.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.Email).IsUnique(true);

                b.Property(u => u.Id).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.Email).IsRequired();
            });

            //Sample test data can be added here :

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Email = "wossewodda-3728@yopmail.com", ValueOne = "Valeur test 1", ValueTwo = "Seconde valeur test" });

        }
    }
}
