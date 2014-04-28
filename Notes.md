# Section Notes on ASP.NET MVC Tutorial
### Source
[Getting Started with ASP.NET MVC 5]
(http://www.asp.net/mvc/tutorials/mvc-5/introduction/getting-started "Microsoft ASP.NET")
## 2. Adding a Controller

**MVC == "Model View Controller"**
<dl>
    <dt>Models</dt>
<dd> Classes that represent the data of the application and use validation
logic to enforce business rules of the application. They are used to store and 
manipulate state & data, typically through objects and databases. The model is 
always independent of the View(s) and Controller(s).
</dd>
<dt>Views</dt>
<dd>Template files that the application uses to dynamically generate
HTML responses. Is simple applications, the View and the Controller may be the
same.</dd>
<dt>Controllers</dt>
<dd> Classes that handle incoming browser requests, retrieve model
data, then specify View templates that return a response to the browser.</dd>
</dl>

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

the `\[Parameter]` portion is taken up by the ID, since it matches the URL 
specification within the `RegisterRoutes()` method. Such parameters are said to 
be passed as *route data*. In ASP.NET MVC applications, passing parameters as route
data is more typical. However, if an application's needs favor a different route
structure be used, one can be added to the `RegisterRoutes()` method; for instance,
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
> Both protect the application from malicious input.

---

## 3. Adding a View

Views in ASP.NET can be created using the **Razor View Engine**, which creates pages
with a *.cshtml* extension. 

### Razor Syntax

Below are a few tips on Razor syntax and rules:

1. Add code to a page using the **@** character
2. Enclose code blocks in braces.
3. Within a block, add a semicolon at the end of each code statement.
4. Variables can be used to store values.
5. String literals are enclosed in double quotation marks.
6. Code is case-sensitive, because d'uh!
7. Most code involves objects, in JSON-like format.
8. Logic blocks & loops are allowed.

 ...The @ character starts inline expressions, single- and multi-line expressions

   ```cshtml
   <!-- Single statement blocks -->
   @{ var total = 7; }

   <!-- Inline statements -->
   <p> The value of your account is: @total </p>

   <!-- Multistatement block -->
   @{
        var prefix = "Welcome to out site!";
        var weekday = DateTime.Now.DayOfWeek;
        var greeting = prefix + " Today is " + weekday;

   }

   <!-- Verbatim string literal prefixed with @ -->
   @{ var filePath = @"C:\Foldername\Path\Ti\File.ext"; }
   <p>The file path is: @filePath</p>

   <!-- Embed quotes using verbatim literal & repeat the quotation marks -->
   @{var quoted = @"The swami says ""Hello, young one!"" "}

   <p>The greting is: @greeting</p>
   ``` 

One can choose to have the Views within an application share a common layout. This
allows the page-specific View files to share a common styling template that 
defines all the shared HTML-related appearance settings, while the Views themselves
only concentrate on the View-specific HTML aspects. This also makes it easy to
change the shared layout settings, as these can all be found (and changed) within
the file that contains the layout. Here, the `/Views/Shared/_Layout.cshtml` file
is the shared layout file. Within the layout template, the `RenderBody()` call
acts as a placeholder for (and renders) the View-specific pages. Each View defined
has, at or near the top, the snippet:

```cshtml
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
```

that explicitly sets the layout page. However, ASP.NET MVC5 also has a file in
`Views/_ViewStart.cshtml` that defines the common layoutfile that all views use,
and has the same code snippet shown above. Therefore, within the View files, we
can safely remove the snippet above and only use it in one place, the `_VieewStart`
file.




### Passing Data from the Controller to the View

Controller classes are invoked in response to an incoming url. They contain the
code that handles the incoming browser requests (from the view), retrieve data
from the database and decide what resonse to send back to the view. The controller
is responsible for providing the data required by the view to render a HTML page.
It performs business logic (as defined by the model) and communicates with the 
database; **the view template should never do any of these**. The view should only
 work with the data provided to it.

As the data/parameters passed from a view to a controller are dynamic, it follows
that the controller should have a dynamic way to pass the response to the view.
For small snippewts of data, ASP.NET MVC does this through a variety of ways, the
most common of which are:
<dl>
    <dt>ViewData</dt>
    <dd>
    This is a property of both the view and the controller that exposes the 
    `ViewDataDictionary` class. To pass data to a view, the data is added to the
    ViewData property of the controller as follows:
    ```c#
    ViewData["key"] = "value";
    ```
    When the view is rendered, the data is copied to the ViewData prooerty of the
    View. Within the view markup, the data can be accessed as follows:
    ```cshtml
    @ViewData["key"]; 
    ```
    Data can be passed from a view back to a controller in much the same way. The
    lifespan of a ViewData object is the current request; all data within it is
    lost when a new request is made. Similarly, if a request is redirected, the
    data is lost. Typecasting and basic null-checking is also the programmer's
    responsibility 
    </dd>
    <dt>ViewBag</dt>
    <dd>
    A ViewBag is a wrapper around a ViewData object that allows the user to create
    dynamic properties within it. As a dynamic object, ViewBag allows for behavior
    to be defined at run time. It doesn't require typecasting or complex null-
    checking, making the addition of dynamic properties far easier. Properties can
    be added to it as follows:
    ```c#
    // here, strings are used for ease
    ViewBag.Key = "Value";
    ```
    and retrieved in an equally easy way within the view:
    ```cshtml
    @ViewBag.Key;
    ```
    Like the ViewData, the ViewBag's lifespan lasts only within the current request,
    and redirection nullifies the data within. 
    Note that since views already expect an explicit model as their data, Viewbags
    and ViewDatas are ideal for transporting extra data that is not included within
    the model, which is one reason both are implemented as properties of both the
    controller and the view.
    </dd>
    <dt>TempData</dt>
    <dd>
    A TempData object is a wrapper for the `TempDataDictionary` object. It was
    developed mainly to deal with the redirecting disadvantages of ViewData and 
    ViewBag, and thus, lasts from creation through the end of the subsequent request.
    It uses the underlying `Session` object to save data in a dictionary-like manner
    (i.e. with string keys and object values). Like the ViewData object, the TempData
    leaves typecasting and null-checking to the programmer. TempData objects come
    in handy when data is to be accessed by multiple actions within the same controller,
    for instance.
    </dd>
    <dt>Session</dt>
    <dd>
    A Session object is used to persist data throughout the current application
    runtime duration. Its lifetime spans all requests, and it too leaves null-
    checking and typecasting to the programmer while wrapping a dictionary underneath.
    </dd> 
</dl>


### Passing data from the View to the Controller

As mentioned, a url invoked from a view typically has the following pattern:
> /Controller/Action/Parameter

The specified controller contains within it a method (here, action) that takes
parameters matching the name and structure of the `/Parameter` portion of the url.
While the url's parameters are typically passed as strings, it's highly likely
that the action acts on objects based on some model, and since ASP puts no
restrictions on the kind of model objects allowed, it must provide a way to map
POST-ed or url values to the expected object. Binding the parameters to a model
is known as **model binding**, and it assists in keeping the application code
clean of code that interrogates the request and its associated environment/context
for data. The default way to do this is through the *DefaultModelBinder*, which
provides a concrete implementation of the *IModelBinder* interface.

