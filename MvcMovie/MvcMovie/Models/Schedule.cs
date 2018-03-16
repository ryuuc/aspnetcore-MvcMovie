using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string PublicSchedule { get; set; }

        [Display(Name = "Public Schedule Size(bytes)")]
        [DisplayFormat(DataFormatString = "{0:N1)")]
        public long PublicScheduleSize { get; set; }

        public string PrivateSchedule { get; set; }

        [Display(Name ="Private Schedule Size(bytes)")]
        [DisplayFormat(DataFormatString ="{0:N1)")]
        public long PrivateScheduleSize { get; set; }

        public DateTime UpdateDT { get; set; }
    }
}
