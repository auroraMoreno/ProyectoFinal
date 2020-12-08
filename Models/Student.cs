using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentDNI { get; set; }
        public int StudentAge { get; set; }
        public string StudentAddress { get; set; }
        public string StudentEmail { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Enrollment Enrollment { get; set; }
        public Grade Grade { get; set; }
    }
}
