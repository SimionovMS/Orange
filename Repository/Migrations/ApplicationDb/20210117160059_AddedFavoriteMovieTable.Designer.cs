﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

namespace Repository.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210117160059_AddedFavoriteMovieTable")]
    partial class AddedFavoriteMovieTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DataAccess.FavoriteMovie", b =>
                {
                    b.Property<long>("FavoriteMovieId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("MovieId");

                    b.Property<string>("UserId");

                    b.HasKey("FavoriteMovieId");

                    b.ToTable("FavoriteMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
