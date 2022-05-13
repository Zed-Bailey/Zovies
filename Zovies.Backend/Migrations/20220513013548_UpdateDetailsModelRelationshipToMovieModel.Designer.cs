﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zovies.Backend.Context;

#nullable disable

namespace Zovies.Backend.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20220513013548_UpdateDetailsModelRelationshipToMovieModel")]
    partial class UpdateDetailsModelRelationshipToMovieModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Zovies.Backend.Models.Details", b =>
                {
                    b.Property<int>("DetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MovieCoverPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MovieFilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MovieGenres")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieID")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Rating")
                        .HasColumnType("REAL");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("DetailsID");

                    b.HasIndex("MovieID")
                        .IsUnique();

                    b.ToTable("MovieDetails");
                });

            modelBuilder.Entity("Zovies.Backend.Models.Movie", b =>
                {
                    b.Property<int>("MovieID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MovieCast")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MovieID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Zovies.Backend.Models.Details", b =>
                {
                    b.HasOne("Zovies.Backend.Models.Movie", "Movie")
                        .WithOne("MovieDetails")
                        .HasForeignKey("Zovies.Backend.Models.Details", "MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Zovies.Backend.Models.Movie", b =>
                {
                    b.Navigation("MovieDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}