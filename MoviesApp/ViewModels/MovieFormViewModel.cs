using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoviesApp.Models;
namespace MoviesApp.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        //Make properties like Movie Model for MovieFormViewModel
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }


        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberInStock { get; set; }

        //property for Title
        public string Title
        {
            get
            {
                //if MovieFormModel =! 0 then "Title=Edit" movie else "Title=New Movie"
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        //Constractor method for defining default value of Id.
        //Contractos method will behave as any field in method
        //.. a constructor is similar to a method that is invoked when an object of the class is created
        //Read More about Constractor : https://www.programiz.com/csharp-programming/constructors
        public MovieFormViewModel()
        {
            Id = 0;
        }

        //This Constractor Assign field in Models.Movie to field in MovieFormViewModel
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }

    }
}