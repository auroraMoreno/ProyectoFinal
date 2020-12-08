using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int FirstCallGrade { get; set; }
        public int SecondCallGrade { get; set; }
        public int ThirdCallGrade { get; set; }
        public int FourthCallGrade { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
