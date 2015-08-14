using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactsBook.DataAccess;
using ContactsBook.Models;
using ContactsBook.Services;

namespace ContactsBook.Controllers
{
    public class PeopleController : Controller
    {		
		private readonly IGenericService _genericService;

		public PeopleController(IGenericService genericService)
		{			
			_genericService = genericService;

		}

		//GET: List People
		public ActionResult ListPeople()
		{
			//var people = _personService.GetAll();
			var people = _genericService.GetAll<Person>();
			return PartialView("_ListPeople", people);
		}



		// GET: People/Details/5
		public ActionResult Details(int id) // Why ???
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Person person = _genericService.GetById<Person>(id);
			if (person == null)
			{
				return HttpNotFound();
			}
			return PartialView("_Details", person);
		}

		// GET: People/Create
		public ActionResult Create()
        {
			return PartialView("_Create");
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PictureUrl,BirthDay")] Person person)
        {
            if (ModelState.IsValid)
            {
				//db.People.Add(person);
				//db.SaveChanges();

				//_personService.AddOrUpdate(person);
				_genericService.AddOrUpdate<Person>(person);
				string url = Url.Action("ListPeople", "People");
				return Json(new { success = true, url = url, target = "#idContact" });
			}

            return PartialView("_Create",person);
		}

        // GET: People/Edit/5
        //public ActionResult Edit(int? id) // WHY ?!?!?!?!?
		public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//Person person = db.People.Find(id);
			Person person =_genericService.GetById<Person>(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit",person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PictureUrl,BirthDay")] Person person)
        {
            if (ModelState.IsValid)
            {

				_genericService.AddOrUpdate<Person>(person);
				string url = Url.Action("ListPeople", "People");
				return Json(new { success = true, url = url, target = "#idContact" });
			}
            return PartialView("_Edit",person);
		}

        // GET: People/Delete/5
        //public ActionResult Delete(int? id) // WHYYYYYYYYY
		public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//Person person = db.People.Find(id);
			Person person = _genericService.GetById<Person>(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete",person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			_genericService.Delete<Person>(id);
			string url = Url.Action("ListPeople", "People");
			return Json(new { success = true, url = url, target = "#idContact" });
		}

    }
}
