<!doctype html>
<html>
  <head>
    <title>F2C conversion</title>
    <style type = "text/css">
      h4 {color: purple;}
      a {color: #151b8d; text-decoration:none;}
      a:visited {color: #151b8d;}
      a:hover {color: #fff; background-color: #666;}
    </style>
  </head>
  <body>
    <!-- Below is a declaration. In effect, it declares two fields, fahr and cent, 
	 each of Java type float. -->
    <%!
       float fahr, cent;
    %>

    <!-- Below is a 'scriplet' (a small junk of standard Java code) to do the conversion.
	 In this example, there's no data validation: we assume the input is ok. -->
    <% 
       String fahrAsString = request.getParameter("tempF").trim();  //*** could be empty string
       fahr = Float.valueOf(fahrAsString);                          //*** could be non-convertible 
       cent = (fahr - 32.0f) * (5.0f / 9.0f);
    %>

    <!-- Below is an old-style JSP expression that writes its value as the HTTP response. -->
    <h4>
      <%= fahr + " in Fahrenheit is " + cent + " in Centigrade." %>
    </h4>
    <p>
      <a href = 'input.html'>Convert another temperature</a>
    </p>
  </body>
</html>
