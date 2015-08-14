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
    public class PhoneTypesController : ApiController
    {
		private readonly IGenericService _genericService;

		public PhoneTypesController(IGenericService genericService)
		{
			_genericService = genericService;
		}

		// GET: api/PhoneTypes
		public IEnumerable<PhoneType> GetPhoneTypes()
        {
			var model = _genericService.GetAll<PhoneType>();
			return model;
        }

        // GET: api/PhoneTypes/5
        [ResponseType(typeof(PhoneType))]
        public IHttpActionResult GetPhoneType(int id)
        {
            PhoneType phoneType = _genericService.GetById<PhoneType>(id);
			if (phoneType == null)
            {
                return NotFound();
            }

            return Ok(phoneType);
        }

        // PUT: api/PhoneTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoneType(int id, PhoneType phoneType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phoneType.Id)
            {
                return BadRequest();
            }


			if (!PhoneTypeExists(id))
			{
				return NotFound();
			}
			else
			{
				_genericService.AddOrUpdate<PhoneType>(phoneType);
			}

			return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PhoneTypes
        [ResponseType(typeof(PhoneType))]
        public IHttpActionResult PostPhoneType(PhoneType phoneType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			_genericService.AddOrUpdate<PhoneType>(phoneType);

			return CreatedAtRoute("DefaultApi", new { id = phoneType.Id }, phoneType);
        }

        // DELETE: api/PhoneTypes/5
        [ResponseType(typeof(PhoneType))]
        public IHttpActionResult DeletePhoneType(int id)
        {
            PhoneType phoneType = _genericService.GetById<PhoneType>(id); ;
            if (phoneType == null)
            {
                return NotFound();
            }

				_genericService.Delete<PhoneType>(id);

				return Ok(phoneType);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool PhoneTypeExists(int id)
        {
			if (_genericService.GetById<PhoneType>(id) == null)
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