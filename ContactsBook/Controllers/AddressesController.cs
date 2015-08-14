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
    public class AddressesController : Controller
    {
	
		private readonly IAddressService _addressService;
		//private readonly IAddressTypeService _addressTypeService;
		private readonly IGenericService _genericService;

		public AddressesController(IAddressService addressService, IGenericService genericService)
		{
			_addressService = addressService;
			//_addressTypeService = addressTypeService;
			_genericService = genericService;
		}
		
		public ActionResult ListAddresses(int id)
		{
			var addresses = _addressService.GetByPersonId(id);			
			ViewBag.PersonId = id;
			return PartialView("_ListAddresses", addresses);
		}

		public ActionResult Create(int personId)
		{
			ViewBag.AddressTypeId = new SelectList(_genericService.GetAll<AddressType>(),"Id","Label");
			ViewBag.PersonId = personId;
            return PartialView("_Create");
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonId,StreetAddress,City,State,Country,ZipCode,AddressTypeId")] Address address)
        {
            if (ModelState.IsValid)
            {                
				_addressService.AddOrUpdate(address);
				string url = Url.Action("ListAddresses", "Addresses", new { Id = address.PersonId });
				return Json(new { success = true, url = url, target="#idAddress" });
			}

            ViewBag.AddressTypeId = new SelectList(_genericService.GetAll<AddressType>(),"Id", "Label", address.AddressTypeId);
            return PartialView("_Create", address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Address address = _addressService.GetOneById(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressTypeId = new SelectList(_genericService.GetAll<AddressType>(),"Id", "Label",address.AddressTypeId);
            return PartialView("_Edit", address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonId,StreetAddress,City,State,Country,ZipCode,AddressTypeId")] Address address)
        {
            if (ModelState.IsValid)
            {

				_addressService.AddOrUpdate(address);
				string url = Url.Action("ListAddresses", "Addresses", new { Id = address.PersonId });
				return Json(new { success = true, url = url, target = "#idAddress" });
			}
            ViewBag.AddressTypeId = new SelectList(_genericService.GetAll<AddressType>(),"Id", "Label",address.AddressTypeId);
            return PartialView("_Edit",address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Address address = _addressService.GetOneById(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete" ,address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Address address = _addressService.GetOneById(id);
			_addressService.Delete(id); ;
			string url = Url.Action("ListAddresses", "Addresses", new { Id = address.PersonId });
			return Json(new { success = true, url = url, target = "#idAddress" });
		}


    }
}
