using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MovieDAL.Models;

namespace MovieDAL.EF
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext() { }
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connString = "Data Source=localhost;Database=CoreMvcMovies;User id=sa;Password=Cla0017!;MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
                entity.Property(e => e.ReleaseDate).HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
                entity.HasIndex(e => e.Title).HasName("IX_Titles").IsUnique();
            });
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
    }
}
