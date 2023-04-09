using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment4_DucMinhHuynh.Models
{
    public class Teacher
    {
        public int teacherid { get; set; }
        public string teacherfname { get; set; }
        public string teacherlname { get; set; }
        public string employeeenumber { get; set; }
        public DateTime hiredate { get; set; }
        public Decimal salary { get; set; }
        public string coursetaught { get; set; }

    }
}