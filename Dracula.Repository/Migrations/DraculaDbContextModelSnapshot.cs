﻿// <auto-generated />
using System;
using Dracula.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dracula.Repository.Migrations
{
    [DbContext(typeof(DraculaDbContext))]
    partial class DraculaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dracula.Domain.Actor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Name");

                    b.Property<Guid?>("NationalityId");

                    b.HasKey("Id");

                    b.HasIndex("NationalityId");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("Dracula.Domain.Casting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActorId");

                    b.Property<Guid?>("FilmId");

                    b.Property<string>("Role")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("FilmId");

                    b.ToTable("Casting");
                });

            modelBuilder.Entity("Dracula.Domain.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Iso");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Dracula.Domain.Film", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CountryId");

                    b.Property<string>("Name");

                    b.Property<int>("ReleaseYear");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("Dracula.Domain.Actor", b =>
                {
                    b.HasOne("Dracula.Domain.Country", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId");
                });

            modelBuilder.Entity("Dracula.Domain.Casting", b =>
                {
                    b.HasOne("Dracula.Domain.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId");

                    b.HasOne("Dracula.Domain.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId");
                });

            modelBuilder.Entity("Dracula.Domain.Film", b =>
                {
                    b.HasOne("Dracula.Domain.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });
#pragma warning restore 612, 618
        }
    }
}
