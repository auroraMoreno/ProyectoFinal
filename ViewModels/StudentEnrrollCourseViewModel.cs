using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.ViewModels
{
    public class StudentEnrrollCourseViewModel
    {
        public  Student Student { get; set; }
        public SelectList StudentList { get; set; }
        public Enrollment Enrollment { get; set; }
        public Course Course { get; set; }
        public SelectList CourseList { get; set; }
    }
}
