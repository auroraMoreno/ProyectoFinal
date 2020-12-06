using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public enum CourseType
    {
        Optional,
        Mandatory
    }
    public class Course
    {
        public int CourseId { get; set; }
        public int Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int YearGrade { get; set; }
        public int Price { get; set; }
        public int SeatCapacity { get; set; }
        public CourseType CourseType { get; set; }
    }
}
