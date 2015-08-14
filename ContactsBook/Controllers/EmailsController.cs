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
    public class EmailsController : Controller
    {

		private readonly IEmailService _emailService;
		//private readonly IEmailTypeService _emailTypeService;
		private readonly IGenericService _genericService;

		public EmailsController(IEmailService emailService, IGenericService genericService)
		{
			_emailService = emailService;
			_genericService = genericService;
		}



		
		public ActionResult ListEmails(int id)
		{
			var emails = _emailService.GetByPersonId(id);
			ViewBag.PersonId = id;
			return PartialView("_ListEmails", emails);
		}



        // GET: Emails/Create
        public ActionResult Create(int personId)
        {
            ViewBag.EmailTypeId = new SelectList(_genericService.GetAll<EmailType>(), "Id", "Label");
			ViewBag.PersonId = personId;
            return PartialView("_Create");
        }

        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonId,EmailAddress,EmailTypeId")] Email email)
        {
            if (ModelState.IsValid)
            {
				_emailService.AddOrUpdate(email);
				string url = Url.Action("ListEmails", "Emails", new { Id = email.PersonId });
				return Json(new { success = true, url = url, target = "#idEmail" });
			}

            ViewBag.EmailTypeId = new SelectList(_genericService.GetAll<EmailType>(), "Id", "Label", email.EmailTypeId);
            return PartialView("_Create", email);
        }

        // GET: Emails/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Email email = _emailService.GetOneById(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmailTypeId = new SelectList(_genericService.GetAll<EmailType>(), "Id", "Label", email.EmailTypeId);
            return PartialView("_Edit",email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonId,EmailAddress,EmailTypeId")] Email email)
        {
            if (ModelState.IsValid)
            {
				_emailService.AddOrUpdate(email);
				string url = Url.Action("ListEmails", "Emails", new { Id = email.PersonId });
				return Json(new { success = true, url = url, target = "#idEmail" });
			}
            ViewBag.EmailTypeId = new SelectList(_genericService.GetAll<EmailType>(), "Id", "Label", email.EmailTypeId);
            return PartialView("_Edit",email);
        }

        // GET: Emails/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Email email = _emailService.GetOneById(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete",email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Email email = _emailService.GetOneById(id);
			_emailService.Delete(id);
			string url = Url.Action("ListEmails", "Emails", new { Id = email.PersonId });
			return Json(new { success = true, url = url, target = "#idEmail" });
		}


    }
}
