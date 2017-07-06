using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieDAL.EF;
using MovieDAL.Models;
using MovieDAL.Repos;

namespace MovieDAL.Initializers
{
    public static class StoreDataInitializer
    {
        public static void InitializeData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<MovieDbContext>();
            InitializeData(context);
        }

        public static void InitializeData(MovieDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            ClearData(context);
            SeedData(context);
        }

        public static void ClearData(MovieDbContext context)
        {
            
            ExecuteDeleteSQL(context, new MovieGenreRepo().GetTableName());
            ResetIdentity(context, new MovieRepo().GetTableName());
            ResetIdentity(context, new MovieGenreRepo().GetTableName());
        }

        public static void ExecuteDeleteSQL(MovieDbContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"Delete from {tableName}");
        }

        public static void ResetIdentity(MovieDbContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"{tableName}\", RESEED, 0);");
        }

        public static IEnumerable<MovieGenre> GetGenres()
        {

            yield return new MovieGenre
            {
                GenreName = "Romantic Comedy"
            };
            yield return new MovieGenre
            {
                GenreName = "Comedy"
            };
            yield return new MovieGenre
            {
                GenreName = "Western"
            };
        }

        public static IEnumerable<Movie> GetMovieData(List<MovieGenre> genres)
        {
            yield return new Movie
            {
                Title = "When Harry Met Sally",
                ReleaseDate = DateTime.Parse("1989-1-11"),
                MovieGenre = genres.First(x=>x.GenreName=="Romantic Comedy"),
                Rating = "PG",
                Price = 7.99M
            };
            yield return new Movie
            {
                Title = "Ghostbusters ",
                ReleaseDate = DateTime.Parse("1984-3-13"),
                MovieGenre = genres.First(x => x.GenreName == "Comedy"),
                Rating = "G",
                Price = 8.99M
            };
            yield return new Movie
            {
                Title = "Ghostbusters 2",
                ReleaseDate = DateTime.Parse("1986-2-23"),
                MovieGenre = genres.First(x => x.GenreName == "Comedy"),
                Rating = "G",
                Price = 9.99M
            };
            yield return new Movie
            {
                Title = "Rio Bravo",
                ReleaseDate = DateTime.Parse("1959-4-15"),
                MovieGenre = genres.First(x => x.GenreName == "Western"),
                Rating = "None",
                Price = 3.99M
            };
        }

        public static void SeedData(MovieDbContext context)
        {
            try
            {
                if (context.Movies.Any()) return;
                context.MovieGenres.AddRange(GetGenres());
                context.SaveChanges();
                context.Movies.AddRange(GetMovieData(context.MovieGenres.ToList()));
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
