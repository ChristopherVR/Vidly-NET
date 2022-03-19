﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieSystem.Infrastructure;

#nullable disable

namespace MovieSystem.API.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20220319082954_GenreInitialData")]
    partial class GenreInitialData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieSystem.Domain.AggregatesModel.GenreAggregate.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres", "Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Rock",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pop",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sci-Fi",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Action",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Thriller",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Comedy",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Horror",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial"
                        });
                });

            modelBuilder.Entity("MovieSystem.Domain.AggregatesModel.MovieAggregate.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DailyRentalRate")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("ImdbUrl")
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<int>("NumberInStock")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies", "Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DailyRentalRate = 5,
                            GenreId = 1,
                            ImdbUrl = "https://www.imdb.com/title/tt0080684/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_15",
                            NumberInStock = 20,
                            Rating = 5,
                            Title = "Star Wars: Episove V - The Empire Strikes Back (1980)",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "Initial"
                        },
                        new
                        {
                            Id = 2,
                            DailyRentalRate = 5,
                            GenreId = 1,
                            ImdbUrl = "https://www.imdb.com/title/tt0167260/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_7",
                            NumberInStock = 20,
                            Rating = 5,
                            Title = "The Lord of the Rings: The Return of the King (2003)",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "Initial"
                        },
                        new
                        {
                            Id = 3,
                            DailyRentalRate = 5,
                            GenreId = 1,
                            ImdbUrl = "https://www.imdb.com/title/tt0468569/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_4",
                            NumberInStock = 20,
                            Rating = 5,
                            Title = "The Godfather: Part II (1974)",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "Initial"
                        },
                        new
                        {
                            Id = 4,
                            DailyRentalRate = 5,
                            GenreId = 1,
                            ImdbUrl = "https://www.imdb.com/title/tt0071562/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_3",
                            NumberInStock = 20,
                            Rating = 5,
                            Title = "The Godfather (1972)",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "Initial"
                        },
                        new
                        {
                            Id = 5,
                            DailyRentalRate = 5,
                            GenreId = 1,
                            ImdbUrl = "https://www.imdb.com/title/tt0068646/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_2",
                            NumberInStock = 20,
                            Rating = 5,
                            Title = "Schindler's List (1993)",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "Initial"
                        },
                        new
                        {
                            Id = 6,
                            DailyRentalRate = 5,
                            GenreId = 1,
                            ImdbUrl = "https://www.imdb.com/title/tt0108052/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_6",
                            NumberInStock = 20,
                            Rating = 5,
                            Title = "Fight Club (1999)",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "Initial"
                        });
                });

            modelBuilder.Entity("MovieSystem.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", "Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HashedPassword = "",
                            Name = "Christopher",
                            Surname = "van Rooyen",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedUser = "initial",
                            Username = "ChristopherVR"
                        });
                });

            modelBuilder.Entity("MovieSystem.Domain.AggregatesModel.UserAggregate.UserFavouriteMovie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<byte>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFavouriteMovies", "Movie");
                });

            modelBuilder.Entity("MovieSystem.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.OwnsOne("MovieSystem.Domain.AggregatesModel.UserAggregate.UserDetails", "UserDetails", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("HomeNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ImageUrl")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PersonalNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "Movie");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = 1,
                                    Address = "12th Avenue nr 17",
                                    HomeNumber = "0000 632198",
                                    PersonalNumber = "+27 79 507 2155"
                                });
                        });

                    b.Navigation("UserDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("MovieSystem.Domain.AggregatesModel.UserAggregate.UserFavouriteMovie", b =>
                {
                    b.HasOne("MovieSystem.Domain.AggregatesModel.UserAggregate.User", null)
                        .WithMany("UserFavouriteMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieSystem.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.Navigation("UserFavouriteMovies");
                });
#pragma warning restore 612, 618
        }
    }
}