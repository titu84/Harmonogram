using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;
using Newtonsoft.Json;
using HarmonogramSzkolny.Models;

namespace HarmonogramSzkolny.Repos
{
    public class Repostory : IRepostory
    {
        public object GetJson()
        {
            return StaticRepo.Data;
        }
        public Title GetTitle()
        {
            return StaticRepo.Title;
        }
        public List<Subject> GetAllSubjects(bool all = false)
        {
            return all? StaticRepo.AllSubjects : StaticRepo.AllSubjects.Where(a => a.Date >= DateTime.Today).ToList();
        }
        public List<Subject> GetShortSubjects(double addDays)
        {
            return StaticRepo.ShortSubjects(addDays);
        }
        public List<string> GetHeaders()
        {
            return StaticRepo.Headers;
        }
    }
}