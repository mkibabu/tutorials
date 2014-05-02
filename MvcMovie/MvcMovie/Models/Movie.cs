using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcMovie.Models
{
    /// <summary>
    /// Primary model, representing movies in the database. Each instance of a
    /// Movie object corresponds to a row within the database.
    /// </summary>
    public class Movie
    {

        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString= "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Rating { get; set; }
    }

    /// <summary>
    /// Represents the Entity Framework movie database context, which handles
    /// CRUD operations for Movie class instances in the database.
    /// </summary>
    public class MovieDBContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}