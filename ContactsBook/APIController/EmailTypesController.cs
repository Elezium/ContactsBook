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
	public class EmailTypesController : ApiController
	{
		private readonly IGenericService _genericService;

		public EmailTypesController(IGenericService genericService)
		{
			_genericService = genericService;
		}

		// GET: api/EmailTypes
		public IEnumerable<EmailType> GetEmailTypes()
		{
			var model = _genericService.GetAll<EmailType>();
			return model;
		}

		// GET: api/EmailTypes/5
		[ResponseType(typeof(EmailType))]
		public IHttpActionResult GetEmailType(int id)
		{
			EmailType emailType = _genericService.GetById<EmailType>(id);
			if (emailType == null)
			{
				return NotFound();
			}

			return Ok(emailType);
		}

		// PUT: api/EmailTypes/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutEmailType(int id, EmailType emailType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != emailType.Id)
			{
				return BadRequest();
			}

			if (!EmailTypeExists(id))
			{
				return NotFound();
			}
			else
			{
				_genericService.AddOrUpdate<EmailType>(emailType);
			}


			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/EmailTypes
		[ResponseType(typeof(EmailType))]
		public IHttpActionResult PostEmailType(EmailType emailType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_genericService.AddOrUpdate<EmailType>(emailType);

			return CreatedAtRoute("DefaultApi", new { id = emailType.Id }, emailType);
		}

		// DELETE: api/EmailTypes/5
		[ResponseType(typeof(EmailType))]
		public IHttpActionResult DeleteEmailType(int id)
		{
			EmailType emailType = _genericService.GetById<EmailType>(id);
			if (emailType == null)
			{
				return NotFound();
			}

			_genericService.Delete<EmailType>(id);

			return Ok(emailType);
		}

		//protected override void Dispose(bool disposing)
		//{
		//    if (disposing)
		//    {
		//        db.Dispose();
		//    }
		//    base.Dispose(disposing);
		//}

		private bool EmailTypeExists(int id)
		{

			if (_genericService.GetById<EmailType>(id) == null)
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