﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieDAL.Models.Base;

namespace MovieDAL.Models
{
    public class Movie : EntityBase
    {

        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [NotMapped]
        public string Genre => MovieGenre?.GenreName;

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }

        [Required]
        public int MovieGenreId { get; set; }
        [ForeignKey(nameof(MovieGenreId))]
        public MovieGenre MovieGenre { get; set; }
    }

}
