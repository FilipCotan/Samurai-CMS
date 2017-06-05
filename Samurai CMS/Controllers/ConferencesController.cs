using System.Drawing.Imaging;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Samurai_CMS.CustomHandlers;
using Samurai_CMS.DAL;
using Samurai_CMS.Models;

namespace Samurai_CMS.Controllers
{
    public class ConferencesController : Controller
    {
        //this has nothing to do with Conference Roles. 
        private const string AdministratorUserName = "Administrator";

        private readonly UnitOfWork _repositories = new UnitOfWork();

        // GET: Conferences
        public ActionResult Index()
        {
            var conferences = _repositories.ConferenceRepository.GetAll();
            ViewBag.IsAdministrator = User.Identity.GetUserName() == AdministratorUserName;

            return View(conferences);
        }

        // GET: Conferences/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.IsAdministrator = User.Identity.GetUserName() == AdministratorUserName;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Conference conference = _repositories.ConferenceRepository.GetById(id);
            if (conference == null)
            {
                return HttpNotFound();
            }

            return View(conference);
        }

        [Authorized(Users = AdministratorUserName)]
        // GET: Conferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Conferences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Theme,PaperIsRequired,ReviewersCount")] Conference conference)
        {
            if (!ModelState.IsValid)
            {
                return View(conference);
            }

            _repositories.ConferenceRepository.Insert(conference);
            _repositories.Complete();

            return RedirectToAction("Index");
        }

        // GET: Conferences/Edit/5
        [Authorized(Users = AdministratorUserName)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var conference = _repositories.ConferenceRepository.GetById(id);
            if (conference == null)
            {
                return HttpNotFound();
            }

            return View(conference);
        }

        // POST: Conferences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Theme,PaperIsRequired,ReviewersCount")] Conference conference)
        {
            if (!ModelState.IsValid)
            {
                return View(conference);
            }

            _repositories.ConferenceRepository.Update(conference);
            _repositories.Complete();

            return RedirectToAction("Index");
        }

        // GET: Conferences/Delete/5
        [Authorized(Users = AdministratorUserName)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Conference conference = _repositories.ConferenceRepository.GetById(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }

        // POST: Conferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repositories.ConferenceRepository.Delete(id);
            _repositories.Complete();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _repositories.Dispose();
            base.Dispose(disposing);
        }
    }
}
