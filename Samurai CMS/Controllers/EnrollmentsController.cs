using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Samurai_CMS.CustomHandlers;
using Samurai_CMS.DAL;
using Samurai_CMS.Models;
using Samurai_CMS.ViewModels;

namespace Samurai_CMS.Controllers
{
    [Authorize]
    public class EnrollmentsController : Controller
    {
        private readonly UnitOfWork _repositories = new UnitOfWork();

        // GET: Enrollments
        [Authorized(Users = "Administrator")]
        public ActionResult Index()
        {
            var enrollments = _repositories.EnrollmentRepository.GetAll(includeProperties: "Edition,Paper,Role,User");

            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var enrollment = _repositories.EnrollmentRepository.GetById(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.EditionTitle = _repositories.EditionRepository.GetById(id).Title;
            ViewBag.RoleId = new SelectList(_repositories.RoleRepository.GetAll(), "Id", "Name");

            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, EnrollmentViewModel enrollmentViewModel)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Conferences");
            }

            AuthorPaper paper = null;

            if (enrollmentViewModel.IsSpeaker)
            {
                var abstractFile = new byte[enrollmentViewModel.Abstract.InputStream.Length];
                enrollmentViewModel.Abstract.InputStream.Read(abstractFile, 0, abstractFile.Length);

                var paperFile = new byte[enrollmentViewModel.Paper.InputStream.Length];
                enrollmentViewModel.Paper.InputStream.Read(paperFile, 0, paperFile.Length);

                paper = new AuthorPaper
                {
                    AbstractFileName = enrollmentViewModel.Abstract.FileName,
                    Abstract = abstractFile,
                    PaperFileName = enrollmentViewModel.Paper.FileName,
                    Paper = paperFile,
                    Authors = enrollmentViewModel.Authors,
                    Keywords = enrollmentViewModel.Keywords,
                    Title = enrollmentViewModel.Title
                };

                _repositories.PaperRepository.Insert(paper); 
            }

            var roles = _repositories.RoleRepository.GetAll();
            var enumerableList = roles as IList<Role> ?? roles.ToList();
            int authorRoleId = enumerableList.First(r => r.Name == Roles.Author.ToString()).Id;
            int listenerRoleId = enumerableList.First(r => r.Name == Roles.Listener.ToString()).Id;

            var enrollment = new Enrollment
            {
                UserId = User.Identity.GetUserId(),
                EditionId = id.Value,
                PaperId = paper?.Id,
                RoleId = enrollmentViewModel.IsSpeaker ? authorRoleId : listenerRoleId,
                Affiliation = enrollmentViewModel.Affiliation
            };

            _repositories.EnrollmentRepository.Insert(enrollment);
            _repositories.Complete();

            return RedirectToAction("Details", "Editions", new { id = id });
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(string userId, int? editionId)
        {
            if (userId == null || editionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            bool isAdministrator = User.Identity.GetUserName() == Roles.Administrator.ToString();

            if (!isAdministrator && User.Identity.GetUserId() != userId)
            {
                return RedirectToAction("NoRights", "Home");
            }

            if (isAdministrator)
            {
                return RedirectToAction("EditAdministrator", new { userId, editionId });
            }

            var enrollment = _repositories.EnrollmentRepository.GetAll(e => e.UserId == userId && e.EditionId == editionId).FirstOrDefault();
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            var enrollmentViewModel = new UpdateEnrollmentViewModel
            {
                Title = enrollment.Paper.Title, 
                Authors = enrollment.Paper.Authors,
                Keywords = enrollment.Paper.Keywords,
                EditionId = enrollment.EditionId,
                PaperId = enrollment.PaperId
            };

            return View(enrollmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateEnrollmentViewModel enrollmentViewModel)
        {
            var paper = _repositories.PaperRepository.GetAll(p => p.Id == enrollmentViewModel.PaperId).First();
            paper.Title = enrollmentViewModel.Title;
            paper.Authors = enrollmentViewModel.Authors;
            paper.Keywords = enrollmentViewModel.Keywords;

            if (enrollmentViewModel.Abstract != null)
            {
                var abstractFile = new byte[enrollmentViewModel.Abstract.InputStream.Length];
                enrollmentViewModel.Abstract.InputStream.Read(abstractFile, 0, abstractFile.Length);

                paper.Abstract = abstractFile;
                paper.AbstractFileName = enrollmentViewModel.Abstract.FileName;
            }

            if (enrollmentViewModel.Paper != null)
            {
                var paperFile = new byte[enrollmentViewModel.Paper.InputStream.Length];
                enrollmentViewModel.Paper.InputStream.Read(paperFile, 0, paperFile.Length);

                paper.Paper = paperFile;
                paper.PaperFileName = enrollmentViewModel.Paper.FileName;
            }

            _repositories.PaperRepository.Update(paper);

            string userId = User.Identity.GetUserId();

            var enrollment = _repositories.EnrollmentRepository.GetAll(e => e.PaperId == paper.Id && e.UserId == userId).First();
            enrollment.Paper = paper;
            _repositories.EnrollmentRepository.Update(enrollment);
            _repositories.Complete();

            return RedirectToAction("Details", "Editions", new { id = enrollmentViewModel.EditionId });
        }

        // GET: Enrollments/EditAdministrator/5
        public ActionResult EditAdministrator(string userId, int? editionId)
        {
            if (userId == null || editionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var enrollment = _repositories.EnrollmentRepository.GetAll(e => e.UserId == userId && e.EditionId == editionId).FirstOrDefault();
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(_repositories.RoleRepository.GetAll(), "Id", "Name");

            return View(enrollment);
        }

        // POST: Enrollments/EditAdministrator/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdministrator([Bind(Include = "RoleId,UserId,EditionId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _repositories.EnrollmentRepository.Update(enrollment);
                _repositories.Complete();

                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(_repositories.RoleRepository.GetAll(), "Id", "Name");

            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var enrollment = _repositories.EnrollmentRepository.GetById(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            _repositories.EnrollmentRepository.Delete(id);
            _repositories.Complete();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositories.Complete();
            }
            base.Dispose(disposing);
        }
    }
}
