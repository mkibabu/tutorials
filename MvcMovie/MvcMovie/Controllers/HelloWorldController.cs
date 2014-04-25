using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public string Index()
        {
            // Note that returning html markup here ties the view and the controller
            // together; generally frowned upon.
            // Also, this action returns a string, while the Welcome action below
            // encodes it. Encoding protects the application from maicious input.
            return "This is the <b>default</b> action...";
        }

        // GET: HelloWorld/Welcome
        public string Welcome(string name, int numTimes = 1)
        {
            string reply = "";
            for (int i = 0; i < numTimes; i++)
            {
                reply += "<p>Hello " + name + "! NumTimes is " + numTimes + ".</p>";
            }

            return HttpUtility.HtmlEncode(reply);
        }
    }
}