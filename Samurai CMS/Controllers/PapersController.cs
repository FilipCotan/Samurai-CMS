using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Samurai_CMS.DAL;
using Samurai_CMS.Models;

namespace Samurai_CMS.Controllers
{
    public class PapersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Papers
        public ActionResult Index()
        {
            var authorPapers = db.AuthorPapers.Include(a => a.Session);
            return View(authorPapers.ToList());
        }

        // GET: Papers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorPaper authorPaper = db.AuthorPapers.Find(id);
            if (authorPaper == null)
            {
                return HttpNotFound();
            }
            return View(authorPaper);
        }

        // GET: Papers/Create
        public ActionResult Create()
        {
            ViewBag.SessionId = new SelectList(db.Sessions, "Id", "Topic");
            return View();
        }

        // POST: Papers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Authors,Keywords,IsAccepted,AbstractFileType,Abstract,PaperFileType,Paper,SessionId")] AuthorPaper authorPaper)
        {
            if (ModelState.IsValid)
            {
                db.AuthorPapers.Add(authorPaper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SessionId = new SelectList(db.Sessions, "Id", "Topic", authorPaper.SessionId);
            return View(authorPaper);
        }

        // GET: Papers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorPaper authorPaper = db.AuthorPapers.Find(id);
            if (authorPaper == null)
            {
                return HttpNotFound();
            }
            ViewBag.SessionId = new SelectList(db.Sessions, "Id", "Topic", authorPaper.SessionId);
            return View(authorPaper);
        }

        // POST: Papers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Authors,Keywords,IsAccepted,AbstractFileName,Abstract,PaperFileName,Paper,SessionId")] AuthorPaper authorPaper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authorPaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SessionId = new SelectList(db.Sessions, "Id", "Topic", authorPaper.SessionId);
            return View(authorPaper);
        }

        // GET: Papers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorPaper authorPaper = db.AuthorPapers.Find(id);
            if (authorPaper == null)
            {
                return HttpNotFound();
            }
            return View(authorPaper);
        }

        // POST: Papers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuthorPaper authorPaper = db.AuthorPapers.Find(id);
            db.AuthorPapers.Remove(authorPaper);
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
