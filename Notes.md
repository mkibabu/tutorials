# Section Notes on ASP.NET MVC Tutorial
### Source
[Getting Started with ASP.NET MVC 5]
(http://www.asp.net/mvc/tutorials/mvc-5/introduction/getting-started "Microsoft ASP.NET")

## 2. Adding a Controller

This section explains the basics of MVC, and sets up a new project.

MVC == **"Model View Controller"**
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
        same.
        </dd>
    <dt>Controllers</dt>
        <dd> Classes that handle incoming browser requests, retrieve model
        data, then specify View templates that return a response to the browser.
        </dd>
</dl>

Hence Model == Data, View == Template & Controller == Operations on data.

MVC invokes a different controller class, and different action method within the
controller class, depending on the incoming url. The url format that determines
which controller to invoke is as follows:
```
/[Controller]/[Action]/[Parameters]
```

This format is set in the *App_Start/RouteConfig.cs* file, as follows:

```c#
public static void RegisterRoutes(RouteCollection routes)
{
    // patterns that shouldn't be checked for matches; here, HttpHandler files
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
> `MethodName(*type* param1, *type* param2)`

the `/[Parameter]` url segment is not used. Instead, the parameters are passed as
 *query strings*, and the `&` symbol acts as a separator of the parameters.

On the other hand, for the url
> /Controller/Action/ID?param1=value1

that corresponds to an action method with the signature
> `MethodName(*type* param1, *type* ID)`

(**note the *ID* in the url and the signature, as well as within `RegisterRoutes()`**)

the `\[Parameter]` portion is taken up by the `ID` value, since it matches the URL 
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

This section explains the intricacies of views and how they are used to cleanly
encapsulate the process of gernerating HTML responses to a client.

Views in ASP.NET can be created using the *Razor View Engine*, which creates pages
with a `.cshtml` extension. 

### Razor Syntax

Below are a few tips on Razor syntax and rules:

1. Add code to a page using the **@** character.

   ...The @ character starts inline expressions, single- and multi-line expressions
2. Enclose code blocks in braces.
3. Within a block, add a semicolon at the end of each code statement.
4. Variables can be used to store values.
5. String literals are enclosed in double quotation marks.
6. Code is case-sensitive, because d'uh!
7. Most code involves objects, in JSON-like format.
8. Logic blocks & loops are allowed.

Examples of these are shown below:

```html
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
   @{ var filePath = @"C:\Foldername\Path\To\File.ext"; }
   <p>The file path is: @filePath</p>

   <!-- Embed quotes using verbatim literal & repeat the quotation marks -->
   @{var quoted = @"The swami says ""Hello, young one!"" "}

   <p>The greeting is: @greeting</p>
``` 

One can choose to have the views within an application share a common layout. This
allows the page-specific view files to share a common styling template that 
defines all the shared HTML-related appearance settings, while the views themselves
only concentrate on the view-specific HTML aspects. This also makes it easy to
change the shared layout settings, as these can all be found (and changed) within
the file that contains the layout. Here, the */Views/Shared/_Layout.cshtml* file
is the shared layout file. Within the layout template, the `RenderBody()` call
acts as a placeholder for (and renders) the view-specific pages. Each view defined
has, at or near the top, the snippet:

```html
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
```

that explicitly sets the layout page. However, ASP.NET MVC5 also has a file in
*Views/_ViewStart.cshtml* that defines the common layoutfile that all views use,
and has the same code snippet shown above. Therefore, within the View files, we
can safely remove the snippet above and only use it in one place, the *_ViewStart*
file.

### Passing Dynamic Data from the Controller to the View

Controller classes are invoked in response to an incoming url. They contain the
code that handles the incoming browser requests (from the view), retrieve data
from the database and decide what resonse to send back to the view. The controller
is responsible for providing the data required by the view to render a HTML page.
It performs business logic (as defined by the model) and communicates with the 
database; **the view template should never do any of these**. The view should only
work with the data provided to it.

As the data/parameters passed from a view to a controller are dynamic, it follows
that the controller should have a convenient late-bound way to pass the response
back to the view. For small snippets of data, ASP.NET MVC does this through a 
variety of ways, the most common of which are:

**1. ViewData**
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
responsibility.
</dd>
**2. ViewBag**
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
**3. TempData**
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
**4. Session**
<dd>
A Session object is used to persist data throughout the current application
runtime duration. Its lifetime spans all requests, and it too leaves null-
checking and typecasting to the programmer while wrapping a dictionary underneath.
</dd>

### Passing data from the View to the Controller

As mentioned, a url invoked from a view typically has the following pattern:

> /Controller/Action/Parameter

The specified controller contains within it a method (here, action) that takes
parameters matching the name and structure of the `/Parameter` portion of the url.
While the url's parameters are typically passed as strings, it's highly likely
that the action acts on objects based on some model, and since ASP puts no
restrictions on the kind of model objects allowed, it must provide a way to map
*POST*-ed or url values to the expected object. Binding the parameters to a model
is known as *model binding*, and it assists in keeping the application code
clean of code that interrogates the request and its associated environment/context
for data. The default way to do this is through the *DefaultModelBinder*, which
provides a concrete implementation of the *IModelBinder* interface.


---


## 4. Adding a Model

This section adds a `Movie` model to the application.

A model is the section of the framework that represents the data within the
application, and the envinronment that goes towards persisting that data to storage.
.NET utilizes the *Entity Framework (EF)* to define model classes. EF is an
object-relational mapping framework, i.e. a framework that generates business objects
and entities according to the  database schema provided. It employs a variety of 
workflows to design and develop data-oriented applications. The main workflows 
used are:

**1. Code First**
<dd>
Allows one to create model objects by writing simple objects, aka *POCO*
*classes* (i.e. *Plain Old CLR Objects*). The classes define the data and
the relationships/mapping between the data, and ASP.NET uses *Migration*
to evolve a database from the classes. Note that reverse-engineering tools
exist that allow one to use Code-First to define data and relationships
that fit into existing databases.
</dd>
**2. Model First**

<dd>
Uses UML diagrams and lines within a designer to describe a new database
structure/schema. The diagrams describe the data while the lines describe
the relationships between the data. From this visual description, ASP.NET
creates the models and business logic that represent the database.
</dd>
**3. Database First**

<dd>
Allows one to reverse-engineer an existing database into UML diagrams
and lines describing the data and relationships between the data. Is
essentially Model-First for existing databases.
</dd>

Using *Code First* paradigm, the following simple classes represent two objects
within the database:

```c#
public class Book
{
    public int BookID { get; set; }
    public string BookName { get; set; }
    public string ISBN { get; set; }
}

public class Review
{
    public int ReviewID { get; set; }
    public int BookID { get; set; }
    public string ReviewText { get; set; }
}
```

Typically, each model class is described within its own file. Along with the simple
POCO class description, the file should also contain, as a separate class, a 
subclass of the `DbContext` class. This subclass would be the portion of the model
that describes the Entity Framework database context of each model. It would
handle the fetching, storing and updating of that POCO data to/from the database.
An example DbContext file for the `Book` class described above would be as follows:

```c#
public class BookDBContext : DbContext
{
    public DbSet<Book> Books { get; set; }
}
```


---


## 5. Creating a Connection String & Working with SQLServer LocalBD

This section describes how to connect to the database created from the model.

The DbContext created for each model handles the database connection and mapping
the data and relationships in the models to the database. EF automatically picks
the *LocalDB* database, a lightweight version of *SQL Server Express Database Engine*,
as the database to use. LocalDB starts on demand, runs in user mode, and saves 
databases as *.mdf* files in the */App_Data/* folder.

(Note that SQL Server Express is not recommended for production environments.)

EF specifies a few ways to specify which database it should connect to:

**1. Calling the DbContext Parameterless Constructor**
<dd>
If no configuration hs been done on the application, , calling the parameterless
constructor on DbContext causes DbContext to create a database and a connection
to it, i.e.

```c#
namespace Demo.EF
{
    public class BloggingContext : DbContext
    {
        public BloggingContext()
        {
            // c# will call parameterless constructor by default
        }
    }
}
```

Here, DbContext will use the qualified name of the derived context class - 
`Demo.EF.BloggingContext` - as the database name and create a connectin to it.
</dd>

**2. Calling the DbContext Constructor with Specified DB Name**
<dd>
If no configuration hs been done on the application, calling the string constructor
on DbContext will create a connection to the database by that name. For instance:

```c#
namespace Demo.EF
{
    public class BloggingContext : DbContext
        : base("BloggingDatabase")
    {
        public BloggingContext()
        {
            
        }
    }
}
```  

Here, a connection string is created with "BloggingDatabase" as the database to
connect to.
</dd>

**3. Specifying Connection String in /Web.config File**
<dd>
In the application root `Web.config` file, one can specify the database that EF
should connect to as a *connection string*. The format of a connection string is
 a semicolon-delimited list of key/value pairs specifying the connection name, the
 data source, authentication, etc.  An example connection string is as follows:

```xml
<add name="ConnectionStringName"
    providerName="System.Data.SqlClient"
    connectionString="Data Source=(LocalDB)\v11.0;
    AttachDbFileName=|DataDirectory|\DatabaseFileName.mdf;
    InitialCatalog=DatabaseName;Integrated Security=True;
    MultipleActiveResultSets=True" />
```

See the [MSDN writeup on connection strings] (http://msdn.microsoft.com/en-us/library/ms254978) for more on their syntax and options.
</dd>


---


## 6. Accessing Models Data from a Controller

This section describes how to create a `MoviesController` class and write code
that retrieves and displays data.

>**NOTE**
> For this section, the observations made are partially dependent on how the 
> controller is created, e.g choosing Code First paradigm, and other options.

At this point, the model `(Movie)` and the DbContext `(MovieDBContext)` already
exist. Creating a `MoviesController` controller based on the model and DbContext
leads to VS2013 creating views for each of the CRUD operations. The automatic
creation of CRUD action methods and their respective views by VS is known as
*scaffolding*.

The initial portion of the MoviesController class is as shown:

```c#
public class MoviesController : Controller
{
    private MovieDBContext db = new MovieDBContext();

```

The controller contains within it an instance of the MovieDBConext class. Recall
that MovieDBContext contains  a `public DbSet<Movie> Movies` property. A `DbSet`
is used to represent an entity that can perform CRUD operations on data,and 
contains methods for tracking what changes have been made to the data and need
to be persisted to the database. Each DbSet (there can be multiple) in a DbContext
holds data of a particular model type. Intuitively, a DbContext corresponds to 
an entire schema (i.e. a collection of tables and views, that may encompass the 
database, and the connection to it), while a DbSet corresponds to a specific table
or view within the database. The DbContext can therefore be used to query, edit
and delete entries within the DbSet(s) it encompasses.

The Index action is as follows:

```c#
// GET: /Movies/
public ActionResult Index()
{
    return View(db.Movies.ToList());
}
```

Here, a call to */Movies/* returns a View that takes a list containing all the
movies in the application.

### Strongly-Typed Models and the @model Keyword

As mentioned earlier, ASP uses a variety of ways to pass late-bound data between
controllers and views, such as the `ViewBag` object. These are dynamic objects to
which data is bound at run-time as properties or, in the case of `ViewData`s and
`TempData`s, as key/value pairs (which is the reason *Intellisense* won't work on
them; no compile-time checking).

MVC also provides the ability to send *strongly-typed* objects to a view template.
This allows compile-time checking, and is the method used by the scaffolding
mechanism to pass data between the MoviesController class and the views generated.

The *Controllers/MoviesController/Details(int)* method is as shown below:

```c#
public ActionResult Details(int? id)
{
    if(id == null)
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
}

Movie movie = db.Movies.Find(id);

if(movie == null)
{
    return HttpNotFound();
}

return View(movie);

```

> **Note**

> `int?` above represents a *Nullable type*, i.e. a type that represents
> all possible values of its type, plus an additional `null` value. A nullable
> type cannot be a reference type.

The `id` parameter is passed as route data, i.e. with the url structure 
*movies/details/1*, to view the details of the movie with id == 1. It could also
be passed as a query string, i.e. *movies/details?id=1*. In both cases, if a `Movie`
is found, an instance of the `Movie` model is passed to the Details view.

The */View/Movies/Details.cshtml* file starts with the following line:

```cshtml
@model MvcMovie.Models.Movie
```

This directive specifies the kind of model that the view should expect. It allows
the view to access the instance that the controller passed by using a strongly-
typed Model object. With this, the view is able to pass the object to `HtmlHelpers`
and access the instance's properties using a JS-property-like syntax, i.e. as
`model => PropertyName`, e.g.

```cshtml
<dt>
    @Html.DisplayNameFor(model => model.Title)
</dt>
<dd>
    @Html.DisplayFor(model => model.Title)
</dd>
<!-- shortened for clarity -->
```
> **Note**

> The expression `Html.DisplayNameFor(model => mode.Title)` has the following parts:
> * `Html` - this is a reference to `System.Web.Mvc.HtmlHelper<TModel>`, a static
> class that provides support for rendering HTML controls in a view. It contains
> static methods (of which `DisplayNameFor()` is one) that help render HTML to 
> the page.
> * `(model => model.Title)` - this is a *lambda expression*, an *anonymous function*
> with the syntax `(input => output)`. While the view has the aforementioned
> `@model MvcMovie.Models.Movie` declaration, the declaration **DOES NOT** mean 
> that wherever the word `model` is used, it is automatically expanded to the 
> instance of `MvcMovie.Models.Movie` passed to the view. Calls to `model` within
> the code are not references to the `@model` within the reference; Note that, to
> render a strongly-typed object, a `ViewPage<TModel>` object is required, and this
> has a `Model` property that is likely what the `@model` declaration refers to. 
> Hence, the lambda expression merely takes something of type `Movie` and returns
> `Movie.Title`; the word `model` within the lambda expression can be changed to
>  anything at all(just like the name of any parameter within a regular function),
>  since the compiler knows that the input to the lambda is of type `Movie`.


Strongly-typed objects are also used in the */Controllers/MoviesController/Index*
method:

```c#
// GET: Movies
public ActionResult Index()
{
    return View(db.Movies.ToList());
}
```

Here, the view gets a list of Movie instances, rather than a single instance. It
therefore must have a declaration that allows it to access an enumerable list
of Movie objects. Thus this declaration at the top of */Views/Movies/Index.cshtml*:

```cshtml
@model IEnumerable<MvcMovie.Models.Movie>
```

This allows the Index view to access the object passed to it as a list of Movie
instances. The view can then, for instance, iterate over them:

```cshtml
@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.Title)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.ReleaseDate)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Genre)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Price)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { id=item.ID }) |
            @Html.ActionLink("Details", "Details", new { id=item.ID }) |
            @Html.ActionLink("Delete", "Delete", new { id=item.ID })
        </td>
    </tr>
}
```

Since the `Model` object is strongly typed (as an `IEnumberable<Movie>` object),
each `item` object in the loop is typed as `Movie`.

> **Note**
> A lambda, by definition, has the format `input => output`. This is true *even*
> *if the lambda doesn't require any input in order to give some output*. Here,
> the call `Html.DisplayFor(modelItem => item.Title)` is used to display the title
> of the `item` instance of the `Movie`. Since `item` is already declared outside
> of the lambda, but within the same scope as the lambda is declared, there is no
> need to pass it as an input to the lambda. Hence `modelItem` is merely a token
> placeholder; irrespective of what we pass to the lambda, `Item.Title` is always
> the output of the lambda, where `item` refers to the `item` within the `foreach`
> line.

### Working with SQL Server LocalDB

Through the *Code-First* paradigm, Entity Framework created a database automatically
from the model class `Movie` that we created. The newly-created database is saved
in */App_Data/Movies.mdf*. By default, EF uses any property named *ID* as the 
database primary key.


---


## 7. Examining the Edit Action and View

This section examines the generated `Edit` actions and view.

### Controlling Appearance of Field Names in Forms

In `Models/Movie/`, the properties of the `Movie` instance are as follows:

```c#
    public class Movie
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
```

When the application is deployed, the views containing forms use the property names
(which are the column names in the Movie table as well) as the labels of any input
controls. Hence the release date is labelled `ReleaseDate`.

To change this, annotate the ReleaseDate field as follows:

```c#
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString= "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
    public DateTime ReleaseDate { get; set; }
```

The `Display` attribute describes how to label form fields. `DataType` specifies
the type of data as a date, so the time portion isn't displayed. The `DisplayFormat`
attribute specifies how to display the date, rather than leaving it to browsers.

### The Edit Action/View

In the Index view, the Edit link created for each movie in the list passed to the
view is generated as follows:

```cshtml
    @HTML.ActionLink("Edit Me", "Edit", new { id=item.id} );
```
`Html` refers to the HtmlHelper mentioned previously. The `ActionLink` method
makes it easy to link to action nethods on controlers. The first argument is the
link text; the second is the naem of the action to invoke, and the third is an
anonymous object that contains/generates the route data. This generates a link
with the format:

> http://movies/edit/id

where *id* is a numerical value representing the id of the movie. The Edit action
called is as follows:

```c#
    // GET: Movies/Edit/5
    public ActionResult Edit(int? id)
    {

        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Movie movie = db.Movies.Find(id);
        if (movie == null)
        {
            return HttpNotFound();
        }
        return View(movie);
    }
```
The method uses the Entity Framework `Find()` method to search for a movie whose
id matches the parameter, and returns it if found.

Given a controller, action and additional data, `HtmlHelpers` use the routes
configured in `App_Start/RouteConfig.cs` to create the link; the route configuration
used is (from the `MapRoute()` method):

```c#
    routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
    );
```

The `Edit` call can also be made via a query string, i.e. as

> http://movies/edit?id=val

where *val* is a numerical id. `App_Start/RouteConfig.MapRoute()` contains a route
entry that matches this format, and the `Movies` controller has an Edit method to
match:

```c#
    // POST: Movies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(movie);
    }
```

The `HttpPost` attribute specifies that the overload Edit method can only be
invoked via *POST* requests. No `HttpGet` attribute is needed for the other Edit
call, though one exists, sicne GET is the default. The `Bind` attribute helps
avoid malicious over-posting; only the properties to be edited are included. The
`ValidateAntiForgeryToken` attribute is ised to prevent forgery of requests, and
is paired with the corresponding `@Html.AntiFogeryToken()` invocation in the Edit
view, which generates hidden form anti-forgery token that must match the Edit
action:

```cshtml
<!-- snippet -->

@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Movie</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        @Html.HiddenFor(model => model.ID)

<!-- snippet -->
```

Both Edit calls return a View that takes a `Movie` model, so the Edit view has
this declaration:

```cshtml
@model MvcMovie.Models.Movie
```

Hence the Edit view expects a the model for the view template to be of type `Movie`.

The Edit view is created through scaffolding when the controller is created, and
it uses a variety of helper methods to create the markup. These include:
<dl>
<dt>`Html.LabelFor`</dt>
<dd>Displays the name of the field, e.g. "Title", "Release Date", etc</dd>
<dt>`Html.EditorFor`</dt>
<dd>Renders a html `<input>` element for the editable field</dd>
<dt>`Html.ValidationMessageFor`</dt>
<dd>Displays any validation message associated with that property/field.</dd>
</dl>

### Processing the POST Request

The `HttpPost` version of the Edit method is as shown below:

```c#
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(movie);
    }
```

The ASP.NET *MVC model binder* takes the posted and bound form values and creates
a `Movie` object that's then passed to the Edit method. The `ModelState.IsValid`
call verifies that the data passed through the form can be used to modify (update
or delete) a `Movie` object. The data is saved to the `Movies` DbSet collection
of the `MovieDBContext` instance `db`, then pushed to the database through the
`SaveChanges()` call. The user is then redirected to the `Index` action.

All the `HttpGet` methods follow the same pattern; model binding creates a `Movie`
object from the data passed along, and pass the model on to the view where needed.
All the CRUD methods have both `HttpGet` and `HttpPost` versions; this is because
`HttpGet` operations are inherently unsafe. Per *REST* principles, *GET* operations
should be safe, have no side effects, and not modify persisted data or change the
state of the application.

---


## 8. Adding Search

This section describes how to add extra functionality to the application, and how
to change the controller and view to suit.

### Updating the Inde Form

Change the `Index` method to the following:

```c#
public ActionResult Index(string searchString)
{
    var movies = from m in db.Movies
                 select m;

    if(!string.IsNullOrEmpty(searchString))
    {
        movies = movies.Where(s => s.Title.Contains(searchString));
    }

    return View(movies);
}
```

The first portion creates a *LINQ* query meant to select all movies in the
database. Then if `searchString` is not blank, the query is modified to filter on
the value of the search string. The query, while defined, has still not been run.

LINQ query executio is deferred until its realized value is actually iterated
over or the `ToList` method is called. Note that the `Contains` method is run on
the database, not the c# code above; it maps to SQL's `LIKE`, which is case
insensitive.

The action above matches url calls that pass the searchString as a query string.
If the parameters are to be passed as route data, the call should be modified to:

```c#
public ActionResult Index(string id)
{
    string searchString = id;
    var movies = from m in db.Movies
                 select m;

    if(!string.IsNullOrEmpty(searchString))
    {
        movies = movies.Where(s => s.Title.Contains(searchString));
    }

    return View(movies);
}
```

and the url woulld appear as */Movies/index/ghost/*, for instance, to search for
movies with the word *ghost* (case insensitive) in their title.

However, rather than using a url, the search functionality requires some UI to
go with it. A simple form can be added to *Index.cshtml* as follows:

```cshtml
@using (Html.BeginForm("Index", "Movies", FormMethod.Get))
{
    <p> 
        Title: @Html.TextBox("SearchString") <br />
        <input type="submit" value="Filter" />
    </p>
}
```

The `Html.BeginForm` helper creates the opening `<form>` tag and causes the form
to post to itself. The parameters added ensure that the url sent contains the
search query string within it, and is therefore bookmark-able; it also ensures 
that the `GET` version of Index is the one called, if a `POST` overload exists.

### Adding Search By Genre

To make it possible to search by both title and genre, the Index method is
modified to (commentary interspersed within code)

```c#
// GET: Movies
public ActionResult Index(string movieGenre, string searchString)
{
    // create a list to hold all the movie genres
    var genreList = new List<String>();

    // create a query to get all genres from the database
    var genreQuery = from g in db.Movies
                     orderby g.Genre
                     select g.Genre;

    // add all distinct genre values into the list
    genreList.AddRange(genreQuery.Distinct());

    // store the list as a SelectList, to use later in a dropdown, and add the
    // selectList to the ViewBag
    ViewBag.movieGenre = new SelectList(genreList);

    // as before, get all movies, and filter by title (within the "if" block)
    var movies = from m in db.Movies
                 select m;

    if(!string.IsNullOrEmpty(searchString))
    {
        movies = movies.Where(s => s.Title.Contains(searchString));
    }

    // constrain the query further to match the genre desired.
    if (!string.IsNullOrEmpty(movieGenre))
    {
        movies = movies.Where(x => x.Genre == movieGenre);
    }

    return View(movies);
}
```


The markup in the *Views/Index.cshtml* file to support the markup would build up
from the previous search functionality markup, to look as follows:

```cshtml
@using (Html.BeginForm("Index", "Movies", FormMethod.Get))
{
    <p> 
        Genre: @Html.DropDownList("movieGenre", "All")
        Title: @Html.TextBox("SearchString") <br />
        <input type="submit" value="Filter" /> 
    </p>
}
```

In the code below:

```cshtml
@Html.DropDownList("MovieGenre", "All");
```

the `movieGenre` parameter provides the key for the `DropDownList` helper to 
access the `IEnumerable<SelectListItem>` list within the `ViewBag` that was
populated within the Index action. Recall that `ViewBag` is merely a wrapper for
`ViewData`, so while primary access to its contents are through a JavaScript-like
`property => value` syntax, the implementation is a key/value pair.

The parameter `All` specifies the value to be preselected within the list. If no
selection is made, the `movieGenre` parameter would be empty, since the posted
data wouldn't send back `All` at it's not in the `SelectList`.


---


## 9. Adding a New Field

This section uses EF Code First migrations to migrate shanges to the model so
that the change is applied to the database.

To avoid obscure errors at runtime, Code First adds a table to the database that
helps keep track of whether the schema of the database is in sync with the model
it was derived from.

### Setting up Code First Migrations for Model Changes

Steps:

1. Delete the *App_Data/Movies.mdf* database file
2. Click *Tools > NuGet/Library Package Manager > Package Manager Console >*
3. In the *Package Manager Console*, at the `PM>` prompt, enter:

> `Enable-Migrations -ContextTypeName MvcMovie.Models.MovieDBContext`

The `Enable-Migrations` command creates a *Migrations/Configuration.cs* file with
a `Seed()` method stub. This method is calle dafter every migration (i.e. calling
`update-database` in the Package Manager) and updates rows that have already been
inserted, or inserts them if they don't exist. This is known as an *upsert* 
operation (an amalgamation of update and insert). A shortened version of the 
method is shown below:

```c#
protected override void Seed(MvcMovie.Models.MovieDBContext context)
{
    context.Movies.AddOrUpdate(i => i.Title,
        new Movie
        {
            Title = "Snatch",
            ReleaseDate = DateTime.Parse("2001-1-19"),
            Genre = "Thriller",
            Price = 12.99M
        }
    );
}
```

The first parameter to the `AddOrUpdate` method specifies the property to use to
check if a row already exists. For this database, its unlikely that movies would
have the same title, so the Title field is used. The next parameter is the Movie
object to add or update to the database.

4. Create a `DbMigration` class for the initial migration. In the Package Manager
Console, enter the command `add-migration Initial` to create the initial migration.
The name `Initial` in the command is arbitrary.

This creates another file `Migrations/{DateStamp}_Initial.cs` that contains the
code to create the database schema (tables, etc). 

5. Update the database. Run `update-database` in the Package Manager Console. This
will run the code in `Migrations/Configuration.cs` to create the table, and the
code in `Migrations/Configuration.Seed(DbContext)` to add the values to the table.
database.

Build and run the application to view the data.



