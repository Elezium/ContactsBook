using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactsBook.DataAccess;
using ContactsBook.Models;
using ContactsBook.Services;

namespace ContactsBook.Controllers
{
	public class ContactsBookController : Controller
	{

		// GET: ContactsBook
		public ActionResult Index()
		{
			return View();
		}
	
	}
}
