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

namespace ContactsBook.APIController
{
	public class EmailsController : ApiController
	{
		private readonly IEmailService _emailService;

		public EmailsController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		// GET: api/Emails
		public IEnumerable<Email> GetEmails()
		{
			return _emailService.GetAll();
		}

		// GET: api/Emails/5
		[ResponseType(typeof(Email))]
		public IEnumerable<Email> GetEmail(int id)
		{
			var email = _emailService.GetByPersonId(id);

			return email;
		}

		// PUT: api/Emails/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutEmail(int id, Email email)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != email.Id)
			{
				return BadRequest();
			}


			if (!EmailExists(id))
			{
				return NotFound();
			}
			else
			{
				_emailService.AddOrUpdate(email);
			}
		

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Emails
		[ResponseType(typeof(Email))]
		public IHttpActionResult PostEmail(Email email)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_emailService.AddOrUpdate(email);

			return CreatedAtRoute("DefaultApi", new { id = email.Id }, email);
		}

		// DELETE: api/Emails/5
		[ResponseType(typeof(Email))]
		public IHttpActionResult DeleteEmail(int id)
		{
			Email email = _emailService.GetOneById(id);
			if (email == null)
			{
				return NotFound();
			}

			_emailService.Delete(id);

			return Ok(email);
		}

		//protected override void Dispose(bool disposing)
		//{
		//    if (disposing)
		//    {
		//        db.Dispose();
		//    }
		//    base.Dispose(disposing);
		//}

		private bool EmailExists(int id)
		{
			if (_emailService.GetOneById(id) == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}