using MovieDAL.Repos.Base;
using MovieDAL.Models;
using System.Collections.Generic;

namespace MovieDAL.Repos
{
    public interface IMovieRepo : IRepo<Movie>
    {
        IList<Movie> GetByGenre(string genre);
        IList<Movie> GetMovies(string movieGenre, string searchString);
    }
}