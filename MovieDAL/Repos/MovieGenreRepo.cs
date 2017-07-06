using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieDAL.EF;
using MovieDAL.Models;
using MovieDAL.Repos.Base;

namespace MovieDAL.Repos
{
    public class MovieGenreRepo : RepoBase<MovieGenre>, IMovieGenreRepo
    {
        public MovieGenreRepo() { }
        public MovieGenreRepo(DbContextOptions<MovieDbContext> options) : base(options) { }
        public IList<string> GetGenres()
        => Table.Select(x => x.GenreName).Distinct().ToList();

    }
}