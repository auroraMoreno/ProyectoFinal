using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    public class TeacherController:Controller
    {
        private readonly ApplicationDbContext _db;
        public TeacherController(ApplicationDbContext db)
        {
            this._db = db;
        }

        //Gestión Profesores 
        public async Task<IActionResult> AllTeachers()
        {
            var teachers = await _db.Teacher.Include(d => d.Department).ToListAsync();
            return View(teachers);
        }

        public async Task<IActionResult> SubjectTeacher(int? id)
        {
            var courses = await _db.Course.Include(c => c.Teacher).Where(x => x.TeacherId == id).ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> RegisterIncident(int? id)
        {
            var teacher = await _db.Teacher.FirstOrDefaultAsync(t => t.TeacherId == id);
            ViewBag.TeacherName = teacher.TeacherName;
            ViewBag.TeacherId = teacher.TeacherId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterIncident(Incidents incident)
        {
            var teacher = await _db.Teacher.SingleOrDefaultAsync(t => t.TeacherId == incident.TeacherId);
            incident.Teacher = teacher;
            _db.Add(incident);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllTeachers");
        }

    }
}
