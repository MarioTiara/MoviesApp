using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesApp.Models;
using MoviesApp.ViewModels;


namespace MoviesApp.Controllers
{
	public class MoviesController : Controller

	{
		//call db form Identity Model
		private ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

	
		public ViewResult Index()
		{
			var movies = _context.Movies.Include(m => m.Genre).ToList();
			return View(movies);
		}

		public ViewResult New()
		{
			var genres = _context.Genres.ToList();
			var viewModel = new MovieFormViewModel
			{
				Genres = genres
			};

			return View("MovieForm", viewModel);
		}

		public ActionResult Edit(int id)
		{
			var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

			if (movie == null)
				return HttpNotFound();

			var viewModel = new MovieFormViewModel(movie)
			{
				Genres = _context.Genres.ToList()
			};

			return View("MovieForm", viewModel);
		}

		public ActionResult Details(int id)
		{
			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
			if (movie == null)
				return HttpNotFound();
			return View(movie);
		}




		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Movie movie)
		{
			//if ModelState isn't valid
			if (!ModelState.IsValid)
			{
				//then MovieFormViewModel will instantiate to viewModel var
				//it also invoke movie to MovieFormViewModel constractor
				var viewModel = new MovieFormViewModel(movie)
				{
					Genres = _context.Genres.ToList()
				};

				return View("MovieForm", viewModel);
			}

			if (movie.Id == 0)
			{
				movie.DateAdded = DateTime.Now;
				_context.Movies.Add(movie);
			}
			else
			{
				var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
				movieInDb.Name = movie.Name;
				movieInDb.GenreId = movie.GenreId;
				movieInDb.NumberInStock = movie.NumberInStock;
				movieInDb.ReleaseDate = movie.ReleaseDate;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Movies");
		}

		
	}
}