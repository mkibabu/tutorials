using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

// .NET uses a data-access framework known as the Entity Framework. It allows for
// model classes to be developed with a Code First paradigm, which allows one to
// create model objects by writing simple classes known as POCOs (Plain Old CLR Objects).
// The database is then derived on the fly from the classes, allowing a clean
// and rapid development process.
namespace MVCMovie.Models
{
    /// <summary>
    /// Represents movies in the database. Each instance of a Movie object will
    /// correspond to a row within the database table, and each property of the
    /// class corresponds to a column within that table.
    /// </summary>
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set;  }
    }

    /// <summary>
    /// Represents the EntityFramework movie database context, which handles
    /// CRUD operations on the Movie class instances within the database. Note
    /// that "context" is used here to mean something that encapsulates state;
    /// hence, MovieDBContext encapsulates the state of the Movie database, the
    /// connection to the underlying data store, and a record of changes to the
    /// objects. Therefore, MovieDBContext holds the state of the Movie database.
    /// It handles the task of connecting to the database and mapping Movie objects
    /// to database records. The conneection info is found in the root Web.config
    /// file (not the /Views/Web.config file).
    /// </summary>
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}