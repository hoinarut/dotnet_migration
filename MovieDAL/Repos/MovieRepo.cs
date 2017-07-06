using MovieDAL.Models;
using MovieDAL.Repos.Base;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MovieDAL.EF;

namespace MovieDAL.Repos
{
    public class MovieRepo : RepoBase<Movie>, IMovieRepo
    {
        public MovieRepo() { }
        public MovieRepo(DbContextOptions<MovieDbContext> options) : base(options) { }
        public override IEnumerable<Movie> GetAll()
        {
            return base.GetAll(x => x.Title, true);
        }
        public IList<Movie> GetByGenre(string genre)
        {
            return GetSome(x => x.MovieGenre.GenreName == genre, x => x.Title, true).ToList();
        }

        public IList<Movie> GetMovies(string movieGenre, string searchString)
        {
            IQueryable<Movie> query = Table;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(movieGenre))
            {
                query = query.Where(x => x.MovieGenre.GenreName == movieGenre);
            }
            return query.OrderBy(s => s.Title).Include(x => x.MovieGenre).ToList();
        }
    }
}