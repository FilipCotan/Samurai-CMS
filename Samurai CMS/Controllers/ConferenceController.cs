using Samurai_CMS.DAL;
using Samurai_CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samurai_CMS.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConferenceController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Conference
        public ActionResult Index()
        {
            var conferences = _context.Conferences.ToList();

            return View(conferences);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Conference model)
        {
            if (ModelState.IsValid)
            {
                _context.Conferences.Add(model);
                _context.SaveChanges();
            }

            return View("Index", _context.Conferences.ToList());
        }
    }
}