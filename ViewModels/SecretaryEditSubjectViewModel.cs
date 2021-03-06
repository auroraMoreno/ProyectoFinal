﻿using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.ViewModels
{
    public class SecretaryEditSubjectViewModel
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public Teacher Teacher { get; set; }
        public SelectList TeacherList { get; set; }
    }
}
