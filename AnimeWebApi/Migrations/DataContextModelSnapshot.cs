﻿// <auto-generated />
using AnimeWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimeWebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimeWebApi.Models.Anime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("directorID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("directorID");

                    b.ToTable("animes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CharacterId = 1,
                            Details = "a panese manga series written and illustrated by Gege Akutami",
                            Genre = "Dark Adventure",
                            Title = "Jujutsu Kaisen",
                            directorID = 1
                        },
                        new
                        {
                            Id = 2,
                            CharacterId = 2,
                            Details = "illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki",
                            Genre = "Action, Adventure,Fantasy",
                            Title = "Naruto",
                            directorID = 2
                        },
                        new
                        {
                            Id = 3,
                            CharacterId = 3,
                            Details = "In the Warring States Period of ancient China (475–221 BCE), Shin and Hyou are war-orphans in the kingdom of Qin",
                            Genre = "Action,Diplomacy,Military",
                            Title = "Kingdom",
                            directorID = 3
                        },
                        new
                        {
                            Id = 4,
                            CharacterId = 4,
                            Details = "Hellsing, a British sponsored secret organisation, is in charge of keeping a check on all vampire",
                            Genre = "Horror Adventure",
                            Title = "Hellsing Ultimate",
                            directorID = 4
                        });
                });

            modelBuilder.Entity("AnimeWebApi.Models.AnimeViewer", b =>
                {
                    b.Property<int>("AnimeId")
                        .HasColumnType("int");

                    b.Property<int>("ViewerId")
                        .HasColumnType("int");

                    b.HasKey("AnimeId", "ViewerId");

                    b.HasIndex("ViewerId");

                    b.ToTable("animeviews");

                    b.HasData(
                        new
                        {
                            AnimeId = 1,
                            ViewerId = 1
                        },
                        new
                        {
                            AnimeId = 1,
                            ViewerId = 2
                        },
                        new
                        {
                            AnimeId = 2,
                            ViewerId = 1
                        },
                        new
                        {
                            AnimeId = 2,
                            ViewerId = 2
                        },
                        new
                        {
                            AnimeId = 2,
                            ViewerId = 3
                        },
                        new
                        {
                            AnimeId = 3,
                            ViewerId = 1
                        },
                        new
                        {
                            AnimeId = 3,
                            ViewerId = 3
                        });
                });

            modelBuilder.Entity("AnimeWebApi.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Hero",
                            Name = "Naruto"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Protagonist",
                            Name = "Gojo"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Climax Hero",
                            Name = "NaNami"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Greatest Anagonist Ever",
                            Name = "Madara Uchicha"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Notorious Vllain",
                            Name = "Shukuna"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Scary Villain & Hero",
                            Name = "Ivar The Boneless"
                        });
                });

            modelBuilder.Entity("AnimeWebApi.Models.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("achievement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("directors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jhon",
                            achievement = "Golden Globe Awards"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Roy",
                            achievement = "CFF"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Shawn",
                            achievement = "CFF"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ar Rhaman",
                            achievement = "Freanch Award"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Roy",
                            achievement = "Indian Award"
                        });
                });

            modelBuilder.Entity("AnimeWebApi.Models.Viewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Critic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("viewers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Critic = "rank 1",
                            Name = "Elite"
                        },
                        new
                        {
                            Id = 2,
                            Critic = "rank 10",
                            Name = "Jackson"
                        },
                        new
                        {
                            Id = 3,
                            Critic = "rank 111",
                            Name = "Hinkle"
                        });
                });

            modelBuilder.Entity("AnimeWebApi.Models.Anime", b =>
                {
                    b.HasOne("AnimeWebApi.Models.Character", "Character")
                        .WithMany("anime")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeWebApi.Models.Director", "director")
                        .WithMany("animes")
                        .HasForeignKey("directorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("director");
                });

            modelBuilder.Entity("AnimeWebApi.Models.AnimeViewer", b =>
                {
                    b.HasOne("AnimeWebApi.Models.Anime", "Anime")
                        .WithMany("AnimeViewers")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeWebApi.Models.Viewer", "Viewer")
                        .WithMany("AnimeViewers")
                        .HasForeignKey("ViewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("Viewer");
                });

            modelBuilder.Entity("AnimeWebApi.Models.Anime", b =>
                {
                    b.Navigation("AnimeViewers");
                });

            modelBuilder.Entity("AnimeWebApi.Models.Character", b =>
                {
                    b.Navigation("anime");
                });

            modelBuilder.Entity("AnimeWebApi.Models.Director", b =>
                {
                    b.Navigation("animes");
                });

            modelBuilder.Entity("AnimeWebApi.Models.Viewer", b =>
                {
                    b.Navigation("AnimeViewers");
                });
#pragma warning restore 612, 618
        }
    }
}
