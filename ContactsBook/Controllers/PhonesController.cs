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
    public class PhonesController : Controller
    {

		private readonly IPhoneService _phoneService;		
		private readonly IGenericService _genericService;

		public PhonesController(IPhoneService addressService, IGenericService genericService)
		{
			_phoneService = addressService;
			_genericService = genericService;
		}
		
		/// <summary>
		/// /	
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ListPhones(int id)
		{
			var phones = _phoneService.GetByPersonId(id);
			ViewBag.PersonId = id;
			return PartialView("_ListPhones", phones);
		}

        // GET: Phones/Create
        public ActionResult Create(int personId)
        {
            ViewBag.PhoneTypeId = new SelectList(_genericService.GetAll<PhoneType>(), "Id", "Label");
			ViewBag.PersonId = personId;
			return PartialView("_Create");
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonId,PhoneNumber,PhoneTypeId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
				_phoneService.AddOrUpdate(phone);
				string url = Url.Action("ListPhones", "Phones", new { Id = phone.PersonId });
				return Json(new { success = true, url = url, target = "#idPhone" });
			}

            ViewBag.PhoneTypeId = new SelectList(_genericService.GetAll<PhoneType>(), "Id", "Label", phone.PhoneTypeId);
            return PartialView("_Create", phone);
        }

        // GET: Phones/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Phone phone = _phoneService.GetOneById(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhoneTypeId = new SelectList(_genericService.GetAll<PhoneType>(), "Id", "Label", phone.PhoneTypeId);
            return PartialView("_Edit",phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonId,PhoneNumber,PhoneTypeId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
				_phoneService.AddOrUpdate(phone);
				string url = Url.Action("ListPhones", "Phones", new { Id = phone.PersonId });
				return Json(new { success = true, url = url, target = "#idPhone" });
			}
            ViewBag.PhoneTypeId = new SelectList(_genericService.GetAll<PhoneType>(), "Id", "Label", phone.PhoneTypeId);
            return PartialView("_Edit",phone);
        }

        // GET: Phones/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Phone phone = _phoneService.GetOneById(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete",phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Phone phone = _phoneService.GetOneById(id);
			_phoneService.Delete(id);
			string url = Url.Action("ListPhones", "Phones", new { Id = phone.PersonId });
			return Json(new { success = true, url = url, target = "#idPhone" });
		}

    }
}
