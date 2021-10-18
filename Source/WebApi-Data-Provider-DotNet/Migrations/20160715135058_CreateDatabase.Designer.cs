using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi_Data_Provider_DotNet.Models;

namespace WebApiDataProviderDotNet.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160715135058_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi_Data_Provider_DotNet.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("ValueOne");

                    b.Property<string>("ValueTwo");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });
        }
    }
}
