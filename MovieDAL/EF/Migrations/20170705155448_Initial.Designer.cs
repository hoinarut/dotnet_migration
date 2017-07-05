using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MovieDAL.EF;

namespace MovieDAL.EF.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20170705155448_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieDAL.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MovieGenreId");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Rating")
                        .HasMaxLength(5);

                    b.Property<DateTime>("ReleaseDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("MovieGenreId");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasName("IX_Titles");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieDAL.Models.MovieGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GenreName");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("MovieDAL.Models.Movie", b =>
                {
                    b.HasOne("MovieDAL.Models.MovieGenre", "MovieGenre")
                        .WithMany("Movies")
                        .HasForeignKey("MovieGenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
