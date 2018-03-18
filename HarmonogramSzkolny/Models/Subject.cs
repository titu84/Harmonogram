using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarmonogramSzkolny.Models
{
    public class Subject
    {        
        public DateTime? Date { get; set; }
        public string Day { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Name { get; set; }
        public string TypeOfSub { get; set; }
        public int? HoursCount { get; set; }
        public string Where { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public string Group3 { get; set; }
        public string Lecturer { get; set; }
        public string ShortDate // zwraca tylko dzień i miesiąc
        {
            get
            {
                if (Date == null)
                    return "";
                string d = Date.Value.Day < 10 ? "0" + Date.Value.Day.ToString() : Date.Value.Day.ToString();
                string m = Date.Value.Month < 10 ? "0" + Date.Value.Month.ToString() : Date.Value.Month.ToString();
                return Date > DateTime.Today.AddYears(-1) ? d + "_" + m : "";
            }
        }
        public string ShortLecturer
        {
            get
            {
                if (String.IsNullOrEmpty(Lecturer))
                    return Lecturer;
                return Lecturer.Substring(0, 4);
            }
        }
        public string ShortDay
        {
            get
            {
                if (String.IsNullOrEmpty(Day))
                    return Day;
                return Day.Substring(0, 4);
            }
        }
        public string ShortName
        {
            get
            {
                if (String.IsNullOrEmpty(Name))
                    return "";
                if (Name.Length > 24)
                    return Name.Substring(0, 24);
                return Name;
            }
        }
        public string BootstrapClassForRow
        {
            get
            {
                if (Date != null && Date.HasValue)
                {
                    return Date.Value.DayOfWeek == DayOfWeek.Friday && FromTime != null ? "info" :
                        Date.Value.DayOfWeek == DayOfWeek.Saturday && FromTime != null ? "success" :
                        Date.Value.DayOfWeek == DayOfWeek.Sunday && FromTime != null ? "warning" :
                        FromTime == null ? "danger text" :
                        "dafault";
                }
                return "default";
            }
            
        }
    }
}