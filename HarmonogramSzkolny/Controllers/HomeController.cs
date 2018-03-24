using HarmonogramSzkolny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonogramSzkolny.Repos;
using System.Web.Mvc;

namespace HarmonogramSzkolny.Controllers
{
    public class HomeController : Controller
    {
        IRepostory r = new Repostory();
        public ActionResult Index()
        {
            try
            {
                return View(r.GetTitle());
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public ActionResult All(bool all = false)
        {
            List<Subject> s = r.GetAllSubjects(all);
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
        public ActionResult Map()
        {
            return View();
        }
        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }
    }
}