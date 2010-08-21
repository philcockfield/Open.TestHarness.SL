To use 'Open.Core.Web' in your ASP.NET MVC project:

1. Reference the assembly, and it's dependencies from your ASP.NET MVC project.
	  - Open.Core.
	  - Open.Core.Contracts
	  - Open.Core.Web
	  - MvcContrib

  2. Add the following line to the Global.ascx.cs file:
			protected void Application_Start()
			{
				...
				MvcContrib.UI.InputBuilder.InputBuilder.BootStrap(); 
			}

3. To reference visual controls, use the HTML extension methods, eg:
            <% Html.Foo(); %>

4. To reference images:
            <img src="/Open.Core/images/KenWilber.png" alt="Ken Wilber" />

5. To reference CSS:
		    <link rel="Stylesheet" href="/Open.Core/Css/Core.css" type="text/css" />

6. To reference JavaScript:
		    <script src="/Open.Core/Scripts/mscorlib.js" type="text/javascript"></script>
			<script src="/Open.Core/Scripts/Open.Core.debug.js" type="text/javascript"></script>

	or to inject this same set of core scripts automatically:
			<% Html.CoreScripts(); %>
