using MovieDAL.Repos.Base;
using MovieDAL.Models;
using System.Collections.Generic;

namespace MovieDAL.Repos
{
    public interface IMovieGenreRepo : IRepo<MovieGenre>
    {
        IList<string> GetGenres();
    }
}