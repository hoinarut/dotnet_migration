using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMVCMovie.Models
{
    public class MovieIndexViewModel
    {
        public IEnumerable<MovieViewModel> Movies { get; set; }
        public string SearchString { get; set; }
        public string MovieGenre { get; set; }
        public List<SelectListItem> MovieGenres { get; set; }
    }
}