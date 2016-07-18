using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using WebApi_Data_Provider_DotNet.Models;

namespace WebApiDataProviderDotNet.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160503140003_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi_Data_Provider_DotNet.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("ValueOne");

                    b.Property<string>("ValueTwo");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();
                });
        }
    }
}
