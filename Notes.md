# Section Notes on ASP.NET MVC Tutorial
### Source
[Getting Startted with ASP.NET MVC 5]
(http://www.asp.net/mvc/tutorials/mvc-5/introduction/getting-started "Microsoft ASP.NET")
## 2. Adding a Controller

MVC == "Model View Controller"
<dl>
<dt>Models</dt>
<dd> Classes that represent the data of the application and use validation
logic to enforce business rules of the application. They are used to store and 
manipulate state & data, typically through objects and databases. The model is 
always independent of the View(s) and Controller(s).
</dd>
<dt>Views<dt>
<dd>Template files that the application uses to dynamically generate
HTML responses. Is simple applications, the View and the Controller may be the
same.</dd>
<dt>Controllers<dt>
<dd> Classes that handle incoming browser requests, retrieve model
data, then specify View templates that return a response to the browser.</dd>
</dt>

Hence Model == Data, View == Template & Controller == Operations on data.

MVC invokes a different controller class, and different action method within the
controller class, depending on the incoming url. The url format that determines
which controller to invoke is as follows:
```
/[Controller]/[Action]/[Parameters]
```

This format is set in the App_Start/RouteConfig.cs file, as follows:
```c#
public static void RegisterRoutes(RouteCollection routes)
{
    // patterns that shouldn't be checked for matches; here, HtthHandler files
    routes.IgnoreRoutes("{resource}.axd/{*pathInfo]");

    // call RouteCollectionExtensions.MapRoute(RouteCollection, Stringx2, Object)
    // to map the url route and set the default route values. Note that it sets
    // the default (i.e. if a url isn't specified) to the Index method of the
    // Home controller.
    routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
    );

}
```

In a url such as the one below:
> /Controller/Action?param1=value1&param2=value2

that corresponds to an action method with the signature 
> MethodName(*type* param1, *type* param2)

the `\[Parameter]` url segment is not used. Instead, the parameters are passed as
 *query strings*, and the `&` symbol acts as a separator of the parameters.

On the other hand, for the url
> /Controller/Action/ID?param1=value1

that corresponds to an action method with the signature
>MethodName(*type* param1, *type* ID)

(**note the *ID* in the url and the signature, as well as within `RegisterRoutes()`**)
the `\[Parameter]` portin is taken up by the ID, since it matches the URL 
specification within the `RegisterRoutes()` method. Such parameters are said to 
be passed as *route data*. In ASP.NET MVC applications, passing parameters as route
data is more typical. However, if an application's needs require a different route
to be specified, one can be added to the `RegisterRoutes()` method; for instance,
a route specification for the param1/ID method can be added by appending the
following code within `RegisterRoutes()`:
```c#
    routes.MapRoute(
        name: "Hello",
        url: "{controller}/{action}/{param1}/{id}"
    );
```


> **Note**
> The method ` HtmlServerUtility.HtmlEncode(string)` is used to make sure a string
> is encoded correctly by the browser and not interpreted as HTML; i.e. `<validTag>`
> would be interpreted as a HTML tag, while `&gt;` and `&lt;` would be interpreted
> as `>` and `<` symbols. Same functionality as `HttpUtility.HtmlEncode(String)`.
> Both protect the application from malicious input