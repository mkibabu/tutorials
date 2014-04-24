using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMovie.Models;
using MVCMovie.MovieDBContext_MVCMovie;

// Through "scaffolding", when a Controller is created with the template option
// "MVC Controller with read/write actions and views, usinf Entity Framework",
// ASP.NET automagically creates the CRUD action methods and the views for each.
namespace MVCMovie.Controllers
{
    public class MoviesController : Controller
    {
        private Models_ db = new Models_();

        //
        // GET: /Movies/

        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /Movies/Details/5

        public ActionResult Details(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // For the edit called from the url
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            // Find(int) is an EntityFramework method
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // For the Edit called from the form, not the url
        // POST: /Movies/Edit/5

        // the model binder, which deals with urls and POSTed params, takes
        // the values passed and creates a Movie object that's passed as the
        // "movie" parameter.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            // verify that the data passed can be used to modify a Movie object
            if (ModelState.IsValid)
            {
                // if the data is valid, save the new info to the Movies collection
                // of the db (MovieDBContext instance) object.
                db.Entry(movie).State = EntityState.Modified;
                // have db persist the changes to the database
                db.SaveChanges();
                // have db redirect user to the Index action of the Movies controller
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //
        // GET: /Movies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // SearchIndex method action, GET
        // allows /Controller/action?param=value urls
        public ActionResult SearchIndex(string movieGenre, string searchString)
        {
            // list to hold all the grnres in the database
            var GenreList = new List<String>();
            // retrieve all the genre values in the database, including duplicates
            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            // append all the distinct Genre values from the query to the end of
            // the list
            GenreList.AddRange(GenreQry.Distinct());
            // then add this list to the ViewBag
            ViewBag.movieGenre = new SelectList(GenreList);
            // now, get all the movies in the database
            var movies = from m in db.Movies
                         select m;
            // filter by the search word, if one exists
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            // if a genre is given, filter/contrain the results to the given genre,
            // else return the movies query results
            if(string.IsNullOrEmpty(movieGenre))
                return View(movies);
            else
                return View(movies.Where(x => x.Genre == movieGenre));
        }

        //// SearchIndex method action, GET
        //// allows /Controller/action?param=value urls
        //public ActionResult SearchIndex(string searchString)
        //{
        //    // create a LINQ query to search the db context
        //    var movies = from m in db.Movies select m;
        //    // if searchString isn't blank, alter the query to include the filter
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        movies = movies.Where(s => s.Title.Contains(searchString));
        //    }
        //    // note that LINQ queries are not executed until the call to iterate
        //    // over the result is made, or the ToList method is called. Thus,
        //    // we return aview that takes the query rather than the query result;
        //    // the query execution is said to be deferred
        //    return View(movies);
        //}

        //// SearchIndex method action, GET
        //// allows /Controller/action/id urls, i.e.
        //// Movies/SearchIndex/idGoesHere
        //public ActionResult SearchIndex(string id)
        //{
        //    string searchString = id;

        //    // create a LINQ query to search the db context
        //    var movies = from m in db.Movies select m;
        //    // if searchString isn't blank, alter the query to include the filter
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        movies = movies.Where(s => s.Title.Contains(searchString));
        //    }
        //    // note that LINQ queries are not executed until the call to iterate
        //    // over the result is made, or the ToList method is called. Thus,
        //    // we return aview that takes the query rather than the query result;
        //    // the query execution is said to be deferred
        //    return View(movies);
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}