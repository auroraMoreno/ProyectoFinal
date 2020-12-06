using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.ViewModels
{
    public class SecretaryAddTeacherViewModel
    {
        public Teacher Teacher { get; set; }
        public Department Department { get; set; }
        public SelectList DepartmentList { get; set; }
    }
}
