﻿using System.Collections.Generic;
using HarmonogramSzkolny.Models;

namespace HarmonogramSzkolny.Repos
{
    public interface IRepostory
    {
        List<Subject> GetAllSubjects(bool all = false);
        List<string> GetHeaders();
        object GetJson();
        List<Subject> GetShortSubjects(double addDays);
        Title GetTitle();
    }
}