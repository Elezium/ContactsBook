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
    public class AddressTypesController : ApiController
    {
		private readonly IGenericService _genericService;

		public AddressTypesController(IGenericService genericService)
		{
			_genericService = genericService;
		}

        // GET: api/AddressTypes
        public IEnumerable<AddressType> GetAddressTypes()
        {
			var model = _genericService.GetAll<AddressType>();
			return model;
        }

        // GET: api/AddressTypes/5
        [ResponseType(typeof(AddressType))]
        public IHttpActionResult GetAddressType(int id)
        {
            AddressType addressType = _genericService.GetById<AddressType>(id);
			if (addressType == null)
            {
                return NotFound();
            }

            return Ok(addressType);
        }

        // PUT: api/AddressTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddressType(int id, AddressType addressType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addressType.Id)
            {
                return BadRequest();
            }

            //db.Entry(addressType).State = EntityState.Modified;
			//_genericService.AddOrUpdate<AddressType>(addressType);

			
                if (!AddressTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
				_genericService.AddOrUpdate<AddressType>(addressType);			
				}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AddressTypes
        [ResponseType(typeof(AddressType))]
        public IHttpActionResult PostAddressType(AddressType addressType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			_genericService.AddOrUpdate<AddressType>(addressType);

			return CreatedAtRoute("DefaultApi", new { id = addressType.Id }, addressType);
        }

        // DELETE: api/AddressTypes/5
        [ResponseType(typeof(AddressType))]
        public IHttpActionResult DeleteAddressType(int id)
        {
			AddressType addressType = _genericService.GetById<AddressType>(id);
            if (addressType == null)
            {
                return NotFound();
            }

			_genericService.Delete<AddressType>(id);

			return Ok(addressType);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool AddressTypeExists(int id)
        {
		if (_genericService.GetById<AddressType>(id) == null)
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