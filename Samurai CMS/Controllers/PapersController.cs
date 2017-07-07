using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Samurai_CMS.DAL;
using Samurai_CMS.Models;
using Samurai_CMS.ViewModels;

namespace Samurai_CMS.Controllers
{
    public class PapersController : Controller
    {
        private readonly UnitOfWork _repositories = new UnitOfWork();
        private readonly Services.EmailService _emailService = new Services.EmailService();

        // GET: Papers
        public ActionResult Index(int? editionId)
        {
            if (editionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userEnrollments =  _repositories.EnrollmentRepository.GetAll(e => e.EditionId == editionId && e.Role.Name == Roles.Author.ToString());
            var authorPapers = new List<AuthorPaper>();
            foreach (var enrollment in userEnrollments.Where(e => e.Paper.IsAccepted == null))
            {
                try
                {
                    var paper = _repositories.PaperRepository.GetAll(p => p.Id == enrollment.PaperId).First();
                    authorPapers.Add(paper);
                }
                catch (Exception e)
                {
                    var msg = e.InnerException.Message;
                    throw;
                }
            }

            return View(authorPapers.ToList());
        }

        // GET: Papers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorPaper authorPaper = _repositories.PaperRepository.GetById(id);
            if (authorPaper == null)
            {
                return HttpNotFound();
            }

            var reviewViewModel = new ReviewPaperViewModel
            {
                Paper = authorPaper
            };

            return View(reviewViewModel);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Details(ReviewPaperViewModel model)
        {
            int paperId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);
            var paper = _repositories.PaperRepository.GetById(paperId);
            paper.IsAccepted = model.IsAccepted;
            _repositories.PaperRepository.Update(paper);

            var review = new ReviewAssignment
            {
                Paper = paper,
                Recommendations = model.Recommendations,
                UserId = User.Identity.GetUserId()
            };
            _repositories.PaperReviewRepository.Insert(review);

            var userEnrollment = _repositories.EnrollmentRepository.GetAll(e => e.PaperId == paperId).First();

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(
                model.IsAccepted
                    ? "Dear {0}, \n\nCongratulations! Your paper was approved! Please consider the following recomandations: \n\n{1}\n\n\n Samurai CMS Team"
                    : "Dear {0}, \n\nWe are sorry to announce you that your paper was rejected! Please consider the following recomandations: \n\n{1}\n\n\n Samurai CMS Team",
                userEnrollment.User.Email, model.Recommendations);

            await _emailService.SendEmailAsync(userEnrollment.User.Email, userEnrollment.User.Name, "Paper Review Result", messageBuilder.ToString());
            _repositories.Complete();

            return RedirectToAction("Index", "Conferences");
        }

        public ActionResult DownloadAbstract(int? paperId)
        {
            if (paperId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var paper = _repositories.PaperRepository.GetById(paperId);
            if (paper == null)
            {
                return HttpNotFound();
            }

            return File(paper.Abstract, System.Net.Mime.MediaTypeNames.Application.Octet, paper.AbstractFileName);
        }

        public ActionResult DownloadPaper(int? paperId)
        {
            if (paperId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var paper = _repositories.PaperRepository.GetById(paperId);
            if (paper == null)
            {
                return HttpNotFound();
            }

            return File(paper.Paper, System.Net.Mime.MediaTypeNames.Application.Octet, paper.PaperFileName);
        }

        // GET: Papers/Create
        public ActionResult Create()
        {
            ViewBag.SessionId = new SelectList(_repositories.SessionRepository.GetAll(), "Id", "Topic");

            return View();
        }

        // POST: Papers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Authors,Keywords,IsAccepted,AbstractFileType,Abstract,PaperFileType,Paper,SessionId")] AuthorPaper authorPaper)
        {
            if (ModelState.IsValid)
            {
                _repositories.PaperRepository.Insert(authorPaper);
                _repositories.Complete();

                return RedirectToAction("Index");
            }

            ViewBag.SessionId = new SelectList(_repositories.SessionRepository.GetAll(), "Id", "Topic", authorPaper.SessionId);
            return View(authorPaper);
        }

        // GET: Papers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var authorPaper = _repositories.PaperRepository.GetById(id);
            if (authorPaper == null)
            {
                return HttpNotFound();
            }
            ViewBag.SessionId = new SelectList(_repositories.SessionRepository.GetAll(), "Id", "Topic", authorPaper.SessionId);
            return View(authorPaper);
        }

        // POST: Papers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Authors,Keywords,IsAccepted,AbstractFileName,Abstract,PaperFileName,Paper,SessionId")] AuthorPaper authorPaper)
        {
            if (ModelState.IsValid)
            {
                _repositories.PaperRepository.Update(authorPaper);
                _repositories.Complete();

                return RedirectToAction("Index");
            }
            ViewBag.SessionId = new SelectList(_repositories.SessionRepository.GetAll(), "Id", "Topic", authorPaper.SessionId);
            return View(authorPaper);
        }

        // GET: Papers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var authorPaper = _repositories.PaperRepository.GetById(id);
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
            _repositories.PaperRepository.Delete(id);
            _repositories.Complete();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositories.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
