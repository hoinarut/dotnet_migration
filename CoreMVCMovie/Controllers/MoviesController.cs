using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using CoreMVCMovie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDAL.EF;
using MovieDAL.Models;
using MovieDAL.Repos;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepo _movieRepo;
        private readonly IMovieGenreRepo _movieGenreRepo;
        private readonly MapperConfiguration _mapperConfiguration;
        public MoviesController(IMovieRepo movieRepo, IMovieGenreRepo movieGenreRepo)
        {
            _movieRepo = movieRepo;
            _movieGenreRepo = movieGenreRepo;
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Movie, MovieViewModel>();
                cfg.CreateMap<MovieViewModel, Movie>().AfterMap((src, dest) =>
                dest.MovieGenre = _movieGenreRepo.Find(x => x.GenreName == src.Genre));
            });
        }


        // GET: /Movies/
        public ActionResult Index(string movieGenre, string searchString)
        {
            List<SelectListItem> movieGenres = _movieGenreRepo.GetGenres()
            .Select(x => new SelectListItem() { Text = x, Value = x })
            .Prepend(new SelectListItem() { Text = "ALL", Value = "" })
            .ToList();

            var movies = _movieRepo.GetMovies(movieGenre, searchString);

            var vm = new MovieIndexViewModel
            {
                MovieGenre = movieGenre,
                SearchString = searchString,
                MovieGenres = movieGenres,
                Movies = _mapperConfiguration.CreateMapper()
                .Map<IList<MovieViewModel>>(movies)
            };

            return View(vm);
        }

        // GET: /Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var movie = _movieRepo.Find(x => x.Id == id, x => x.MovieGenre);
            if (movie == null)
            {
                return NotFound();
            }
            var vm = _mapperConfiguration.CreateMapper().Map<MovieViewModel>(movie);
            return View(vm);
        }

        // GET: /Movies/Create
        public ActionResult Create()
        {
            ViewBag.Genres = _movieGenreRepo.GetGenres()
            .Select(x => new SelectListItem() { Text = x, Value = x })
            .Prepend(new SelectListItem() { Text = "ALL", Value = "" })
            .ToList();
            return View(new MovieViewModel
            {
                Genre = "Comedy",
                Price = 3.99M,
                ReleaseDate = DateTime.Now,
                Rating = "G",
                Title = "Ghotst Busters III"
            });
        }

        // POST: /Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "ID,Title,ReleaseDate,Genre,Price,Rating")] MovieViewModel movieVm)
        {
            if (!ModelState.IsValid)
            {
                return View(movieVm);
            }
            var movie = _mapperConfiguration.CreateMapper().Map<Movie>(movieVm);
            // movie.MovieGenre = _movieGenreRepo.Find(x => x.GenreName == movieVm.Genre);
            // _movieRepo.Add(movie);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Genres = _movieGenreRepo.GetGenres()
            .Select(x => new SelectListItem() { Text = x, Value = x })
            .Prepend(new SelectListItem() { Text = "ALL", Value = "" })
            .ToList();

            if (id == null)
            {
                return BadRequest();
            }
            Movie movie = _movieRepo.Find(x => x.Id == id, x => x.MovieGenre);
            if (movie == null)
            {
                return NotFound();
            }
            var vm = _mapperConfiguration.CreateMapper().Map<MovieViewModel>(movie);
            return View(vm);
        }

        // POST: /Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "ID,Title,ReleaseDate,Genre,Price,Rating,Timestamp")] MovieViewModel movieVm)
        {
            if (!ModelState.IsValid)
            {
                return View(movieVm);
            }
            var movie = _mapperConfiguration.CreateMapper().Map<Movie>(movieVm);
            _movieRepo.Update(movie);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Movie movie = _movieRepo.Find(x => x.Id == id, x => x.MovieGenre);
            if (movie == null)
            {
                return NotFound();
            }
            var vm = _mapperConfiguration.CreateMapper().Map<MovieViewModel>(movie);
            return View(vm);
        }

        // POST: /Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, byte[] timestamp)
        {
            _movieRepo.Delete(id, timestamp);
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _movieRepo.Dispose();
                _movieGenreRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
