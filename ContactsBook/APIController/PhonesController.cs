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
    public class PhonesController : ApiController
    {
		private readonly IPhoneService _phoneService;

		public PhonesController(IPhoneService phoneService)
		{
			_phoneService = phoneService;
		}

		// GET: api/Phones
		public IEnumerable<Phone> GetPhones()
        {
			return _phoneService.GetAll();
        }

        // GET: api/Phones/5
        [ResponseType(typeof(Phone))]
        public IEnumerable<Phone> GetPhone(int id)
        {
			var phone = _phoneService.GetByPersonId(id);
            
            return phone;
        }

        // PUT: api/Phones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhone(int id, Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phone.Id)
            {
                return BadRequest();
            }

                if (!PhoneExists(id))
				{
					return NotFound();
				}
				else
				{
					_phoneService.AddOrUpdate(phone);
				}

				return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Phones
        [ResponseType(typeof(Phone))]
        public IHttpActionResult PostPhone(Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			_phoneService.AddOrUpdate(phone);

            return CreatedAtRoute("DefaultApi", new { id = phone.Id }, phone);
        }

        // DELETE: api/Phones/5
        [ResponseType(typeof(Phone))]
        public IHttpActionResult DeletePhone(int id)
        {
			Phone phone = _phoneService.GetOneById(id);
            if (phone == null)
            {
                return NotFound();
            }

			_phoneService.Delete(id);

            return Ok(phone);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool PhoneExists(int id)
        {
			if (_phoneService.GetOneById(id) == null)
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