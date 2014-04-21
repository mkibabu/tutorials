using System.Web;
using System.Web.Mvc;

namespace MVCMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: /HelloWorld/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /HelloWorld/Welcome
        // <summary>
        // Example of a poorly-designed controller. The parameters are passed
        // to the Controller from the View as either a POST-ed form or as a URL
        // in the form /[Controller]/[Action][?Parameter(s)]. Here, name and
        // numTimes represent the parameters that would be passed, and a url
        // example that matches would be /HelloWorld/Welcome?name=Michael&numTimes=4
        // </summary>

        //public string Welcome(string name, int numTimes = 1)
        //{
        //    string ret = "Hello " + name + "!</br>";

        //    for (int i = 0; i < numTimes; i++)
        //    {
        //        ret += "This is the Welcome action method...</br>";
        //    }
        //    return ret;
        //}

        
        // GET: /HelloWorld/Welcome
        /// <summary>
        /// This method/action represents a better way to pass data from a 
        /// Controller to a View. Rather than have rthe Controller spew out the
        /// html/View as the commented out method above does, this one adds the
        /// parameters passed (i.e. the data to transfer) to the ViewBag. Note 
        /// that a ViewBag is a dynamic object; it has no defined properties
        /// until you put something in it.
        /// </summary>
        
        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name + "!";
            ViewBag.NumTimes = numTimes;
            return View();
        }

    }
}
