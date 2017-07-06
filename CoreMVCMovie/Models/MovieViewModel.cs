using System;
using CoreMVCMovie.Validation;
using MovieDAL.Models;

namespace CoreMVCMovie.Models
{
    public class MovieViewModel : Movie
    {
        public new string Genre { get; set; }
        [DateMustBeGreaterThan("12/31/1969")]
        public override DateTime ReleaseDate { get; set; }
    }
}