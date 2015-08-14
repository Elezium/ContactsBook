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
    public class AddressesController : ApiController
    {
		private readonly IAddressService _addressService;

		public AddressesController(IAddressService addressService)
		{
			_addressService = addressService;
		}

		// GET: api/Addresses
		public IEnumerable<Address> GetAddresses()
        {
			return _addressService.GetAll();
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IEnumerable<Address> GetAddress(int id)
        {
			var address = _addressService.GetByPersonId(id);
			return address;
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }



			if (!AddressExists(id))
			{
				return NotFound();
			}
			else
			{
				_addressService.AddOrUpdate(address);
			}


            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


			_addressService.AddOrUpdate(address);

			return CreatedAtRoute("DefaultApi", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
			Address address = _addressService.GetOneById(id);
            if (address == null)
            {
                return NotFound();
            }

			_addressService.Delete(id);

            return Ok(address);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool AddressExists(int id)
        {
			if (_addressService.GetOneById(id) == null)
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