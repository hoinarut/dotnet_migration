using System.Collections.Generic;
using MovieDAL.Models.Base;

namespace MovieDAL.Models
{
    public class MovieGenre : EntityBase
    {
        public string GenreName { get; set; }
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}