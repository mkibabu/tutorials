﻿using System;
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

        // The method below passes its parameters as a query string, therefore not
        // using the /[Parameter] section of the url. Query string format ==
        // /Controller/Action?param1=value1&param2=value2
        // GET: HelloWorld/Welcome
        //public string Welcome(string name, int numTimes = 1)
        //{
        //    return HttpUtility.HtmlEncode("Hello " + name + "! NumTimes is " + numTimes);
        //}

        // The method below passes its parameters using the routes mapped in
        // App_Start/RouteConfig.RegisterRoutes(RouteCollection). This is because the parameter
        // matche the names of the route sections used in the second mapped route.
        // Later configur
        // GET: HelloWorld/Welcome
        public string Welcome(string name, int ID = 1)
        {
            return HttpUtility.HtmlEncode("Hello " + name + "! ID is " + ID);
        }
    }
}