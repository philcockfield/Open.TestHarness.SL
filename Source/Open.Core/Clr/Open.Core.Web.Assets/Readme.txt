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


3. To reference icons:
            <img src="/Open.Assets/Icons/Silk/SilkAccept.png"  />
