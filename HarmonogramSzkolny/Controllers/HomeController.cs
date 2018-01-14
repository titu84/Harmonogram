using HarmonogramSzkolny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HarmonogramSzkolny.Controllers
{
    public class HomeController : Controller
    {
        Repos.Repostory r = new Repos.Repostory();
        public ActionResult Index()
        {   
            return View(r.GetTitle());
        }

        public ActionResult All()
        {
            List<Subject> s = r.GetAllSubjects();
            ViewBag.Headers = r.GetHeaders();
            return View(s);
        }
        public ActionResult AllMobile()
        {
            List<Subject> s = r.GetAllSubjects();           
            return View(s);
        }

        public ActionResult Today()
        {
            List<Subject> s = r.GetShortSubjects(0);
            ViewBag.Headers = r.GetHeaders();
            return View("All", s);
        }
        public ActionResult Tomorrow()
        {
            List<Subject> s = r.GetShortSubjects(1);
            ViewBag.Headers = r.GetHeaders();
            return View("All", s);
        }
        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }
    }
}