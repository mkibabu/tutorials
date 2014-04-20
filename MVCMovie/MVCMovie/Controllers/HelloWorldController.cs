using System.Web;
using System.Web.Mvc;

namespace MVCMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: /HelloWorld/

        public string Index()
        {
            return "This is my <b>default</b> action...";
        }

        //
        // GET: /HelloWorld/Welcome

        public string Welcome(string name, int numTimes = 1)
        {
            string ret = "Hello " + name + "!</br>";

            for (int i = 0; i < numTimes; i++)
            {
                ret += "This is the Welcome action method...</br>";
            }
            return ret;
        }

    }
}
