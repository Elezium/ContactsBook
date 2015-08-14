using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContactsBook.DataAccess;
using ContactsBook.Models;
using ContactsBook.Services;
using System.Web.Http.Cors;

namespace ContactsBook.APIController
{

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class PeopleController : ApiController
	{
	   

		private readonly IGenericService _genericService;

		public PeopleController(GenericService genericService)
		{
			_genericService = genericService;
		}

		// GET: api/People
		//public IQueryable<Person> GetPeople()
		public IEnumerable<Person> GetPeople()
		{
			IEnumerable<Person> model = _genericService.GetAll<Person>();
			return model;
		}

		// GET: api/People/5
		[ResponseType(typeof(Person))]
		public IHttpActionResult GetPerson(int id)
		{
			Person person = _genericService.GetById<Person>(id);
			if (person == null)
			{
				return NotFound();
			}

			return Ok(person);
		}

		// PUT: api/People/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutPerson(int id, Person person)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != person.Id)
			{
				return BadRequest();
			}


			if (! _genericService.Exists<Person>(id))
			{
				return NotFound();
			}
			else
			{
				_genericService.AddOrUpdate<Person>(person);
			}			

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/People
		[ResponseType(typeof(Person))]
		public IHttpActionResult PostPerson(Person person)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//db.People.Add(person);
			//db.SaveChanges();
			_genericService.AddOrUpdate<Person>(person);

			return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
		}

		// DELETE: api/People/5
		[ResponseType(typeof(Person))]
		public IHttpActionResult DeletePerson(int id)
		{
			//Person person = db.People.Find(id);
			var person = _genericService.GetById<Person>(id);
			if (person == null)
			{
				return NotFound();
			}

			//db.People.Remove(person);
			//db.SaveChanges();
			_genericService.Delete<Person>(id);

			return Ok(person);
		}

		//protected override void Dispose(bool disposing)
		//{
		//    if (disposing)
		//    {
		//        db.Dispose();
		//    }
		//    base.Dispose(disposing);
		//}

		//private bool PersonExists(int id)
		//{
		//	// Ugly, but tired.  
		//	var t = _genericService.GetById<Person>(id);
			
  //          if (t == null) {								
		//		return false;
		//	}
		//	else
		//	{
		//		return true;
		//	}
			
			//return db.People.Count(e => e.Id == id) > 0;
		}

}