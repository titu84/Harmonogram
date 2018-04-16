using HarmonogramSzkolny.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace HarmonogramSzkolny.Repos
{
    internal static class StaticRepo
    {
        private static dynamic data;
        private static List<Subject> allSubjects;
        private static List<string> headers;
        private static void fixData()
        {
            if (data == null)
            {
                data = JsonConvert.DeserializeObject(File.ReadAllText(HostingEnvironment.MapPath(@"~\App_Data\excelData.json")));                            
            }
            if (allSubjects == null)
            {
                convertAllSubjects();
            }
            if (headers == null)
            {
                getHeaders();
            }
        }
        private static void getHeaders()
        {
            List<string> l = new List<string>();
            for (int i = 0; i < 12; i++)
            {
                string a = data[2][i].ToString();
                a = a.Replace(" ", "_");
                if (a.Length > 9)
                    a = a.Substring(0, 9);
                l.Add(a);
            }
            headers = l;
        }
        private static void convertAllSubjects()
        {
            List<Subject> l = new List<Subject>();
                    
            for (int i = 3; i < data.Count; i++) // 0,1 i 2 to tytuły i nagłówki
            {
                try
                {
                    Subject s = new Subject();
                    if (data[i][0] > 0)
                        s.Date = new DateTime(1900, 1, 1).AddDays(-2).AddDays((double)data[i][0]);
                    s.Day = data[i][1];
                    if (data[i][0] > 0 && data[i][2] > 0)
                        s.FromTime = new DateTime(1900, 1, 1).AddDays(-2).AddDays((double)data[i][0]).AddDays((double)data[i][2]).ToString("HH:mm");
                    if (data[i][0] > 0 && data[i][3] > 0)
                        s.ToTime = new DateTime(1900, 1, 1).AddDays(-2).AddDays((double)data[i][0]).AddDays((double)data[i][3]).ToString("HH:mm");
                    s.Name = data[i][4];
                    s.TypeOfSub = data[i][5];
                    s.HoursCount = data[i][6];
                    s.Where = $"{data[i][7]} {data[i][8]}";
                    s.Group1 = data[i][9];
                    s.Group2 = data[i][10];
                    s.Group3 = data[i][11];
                    s.Lecturer = data[i][12];

                    if (s.Date != null ||
                        s.Day != null ||
                        s.Name != null ||
                        s.TypeOfSub != null ||
                        s.FromTime != null ||
                        s.ToTime != null ||
                        s.HoursCount != null ||
                        s.Where != null ||
                        s.Group1 != null ||
                        s.Group2 != null ||
                        s.Group3 != null ||
                        s.Lecturer != null
                    )
                    {
                        try
                        {
                            var t = data[i][2] - data[i - 1][3];
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            throw;
                        }

                        if (i > 3 && data[i][0] == data[i - 1][0] && data[i][2] - data[i - 1][3] > 0.0416 && data[i][2] - data[i - 1][3] < 5) //3 pierwszy piersz, 0.416 godzina, 5 to 12h
                        {
                            var ts = new DateTime(1900, 1, 1).AddDays(-2).AddDays((double) data[i][0])
                                            .AddDays((double) data[i][2]) -
                                        new DateTime(1900, 1, 1).AddDays(-2).AddDays((double) data[i - 1][0])
                                            .AddDays((double) data[i - 1][3]);
                            string breakTime = ts.TotalMinutes - ((int)ts.TotalHours * 60) != 0d?
                                $"{(int)ts.TotalHours}:{ts.TotalMinutes - ((int)ts.TotalHours * 60)}h":
                                $"{(int)ts.TotalHours}h";
                            l.Add(new Subject()
                            {
                                Date = s.Date,
                                Day = s.Day,
                                Name = $"OKNO {breakTime}",
                                TypeOfSub = null,
                                FromTime = new DateTime(1900, 1, 1).AddDays(-2).AddDays((double)data[i-1][0]).AddDays((double)data[i-1][3]).AddSeconds(+1).ToString("HH:mm"),
                                ToTime = new DateTime(1900, 1, 1).AddDays(-2).AddDays((double)data[i][0]).AddDays((double)data[i][2]).ToString("HH:mm"),
                                HoursCount = null,
                                Where = null,
                                Group1 = null,
                                Group2 = null,
                                Group3 = null,
                                Lecturer = null
                            });
                        }
                        l.Add(s);
                    }

                }
                catch (Exception ex)
                {
                    //todo zalogować
                }
            }
            allSubjects = l;
        }
        internal static dynamic Data
        {
            get
            {
                fixData();
                return data;
            }
        }
        internal static Title Title
        {
            get
            {
                fixData();
                return new Title { MainTitle = data[0][0], Description = data[1][0] };
            }
        }
        internal static List<string> Headers
        {
            get
            {
                fixData();
                return headers;
            }
        }
        internal static List<Subject> AllSubjects
        {
            get
            {
                fixData();
                return allSubjects;
            }
        }
        internal static List<Subject> ShortSubjects(double addDays) // 0 today, 1 tomorrow
        {
            fixData();
            return allSubjects.Where(a => a.Date == DateTime.Today.AddDays(addDays)).ToList();
        }
    }
}
