﻿// <auto-generated />
using System;
using API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    [DbContext(typeof(LocationHistoryDbContext))]
    [Migration("20200316120545_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("API.Database.Entities.UserLocations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTimeUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Latitude")
                        .HasColumnType("integer");

                    b.Property<int>("Longitude")
                        .HasColumnType("integer");

                    b.Property<string>("UserIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UsersLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
