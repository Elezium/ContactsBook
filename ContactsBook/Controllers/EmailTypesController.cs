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

namespace ContactsBook.Controllers
{
    public class EmailTypesController : Controller
    {
        private ContactsBookDBContext db = new ContactsBookDBContext();

        // GET: EmailTypes
        public ActionResult Index()
        {
            return View(db.EmailTypes.ToList());
        }

        // GET: EmailTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailType emailType = db.EmailTypes.Find(id);
            if (emailType == null)
            {
                return HttpNotFound();
            }
            return View(emailType);
        }

        // GET: EmailTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Label")] EmailType emailType)
        {
            if (ModelState.IsValid)
            {
                db.EmailTypes.Add(emailType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emailType);
        }

        // GET: EmailTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailType emailType = db.EmailTypes.Find(id);
            if (emailType == null)
            {
                return HttpNotFound();
            }
            return View(emailType);
        }

        // POST: EmailTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Label")] EmailType emailType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailType);
        }

        // GET: EmailTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailType emailType = db.EmailTypes.Find(id);
            if (emailType == null)
            {
                return HttpNotFound();
            }
            return View(emailType);
        }

        // POST: EmailTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailType emailType = db.EmailTypes.Find(id);
            db.EmailTypes.Remove(emailType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
