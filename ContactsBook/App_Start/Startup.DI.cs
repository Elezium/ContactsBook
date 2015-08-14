using Owin;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ContactsBook.DataAccess;
using ContactsBook.Models;
using ContactsBook.Services;
using System.Web.Http;

namespace ContactsBook
{
	public partial class Startup
	{
		public void ConfigureDependencyInjection(IAppBuilder app)
		{
			// Create a new Simple Injector container 
			var container = new Container();

			// Configure the container (register dependencies) 
			InitializeContainer(container);

			// Register all MVC controllers 
			// Fail to register the AccountController.  Let's remove DI from Identity for now. 
			//container.RegisterMvcControllers(Assembly.GetExecutingAssembly()); 

			//List all Controller and register them, one by one. 
			//var registeredControllerTypes = 
			//	SimpleInjectorMvcExtensions.GetControllerTypesToRegister( 
			//	container, Assembly.GetExecutingAssembly()); 

			//foreach (var controllerType in registeredControllerTypes) 
			//{ 
			//	container.Register(controllerType, controllerType, Lifestyle.Transient); 
			//} 

			//List all controller, except... the exeception 
			var registeredControllerTypes =
			  SimpleInjectorMvcExtensions.GetControllerTypesToRegister(
				container, Assembly.GetExecutingAssembly())
				 .Where(type => type.Name != "AccountController").
				 Where(type => type.Name != "ManageController");

			foreach (var controllerType in registeredControllerTypes)
			{
				container.Register(controllerType, controllerType, Lifestyle.Transient);
			}

			//WEB API.
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			// Optionally verify the container's configuration. 
			container.Verify();

			// Register the container as MVC DependencyResolver. 
			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

			GlobalConfiguration.Configuration.DependencyResolver =
		new SimpleInjectorWebApiDependencyResolver(container);
		}


		private static void InitializeContainer(Container container)
		{
			// DbContext, UoW & Repository 
			container.RegisterPerWebRequest<DbContext, ContactsBookDBContext>();
			container.RegisterPerWebRequest<IUnitOfWork, UnitOfWork>();
			container.Register<IRepository, GenericRepository>();
			//container.Register<IPersonService, PersonService>();
			container.Register<IAddressService, AddressService>();
			container.Register<IEmailService,ContactsBook.Services.EmailService>();
			container.Register<IPhoneService, PhoneService>();
			//container.Register<IAddressTypeService, AddressTypeService>();
			//container.Register<IEmailTypeService, EmailTypeService>();
			//container.Register<IPhoneTypeService, PhoneTypeService>();
			container.Register<IGenericService, GenericService>();

		



			// Services 
			// TODO 
		}

	}
}