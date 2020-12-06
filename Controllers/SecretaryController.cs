using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ProyectoFinal.Controllers
{
    public class SecretaryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SecretaryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Gestión departamentos
        public async Task<IActionResult> AllDepartments()
        {
            var departments = await _db.Department.ToListAsync();
            return View(departments);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            _db.Add(department);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllDepartments");
        }

        //Gestión Profesores 
        public async Task<IActionResult> AllTeachers()
        {
            var teachers = await _db.Teacher.Include(d => d.Department).ToListAsync();
            return View(teachers);
        }

        public async Task<IActionResult> AddTeacher()
        {
            var departmentDisplay = await _db.Department.Select(x => new
            {
                Id = x.DepartmentId,
                Value = x.DepartmentName
            }).ToListAsync();
            SecretaryAddTeacherViewModel vm = new SecretaryAddTeacherViewModel();
            vm.DepartmentList = new SelectList(departmentDisplay, "Id", "Value");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(SecretaryAddTeacherViewModel vm)
        {
            var department = await _db.Department.SingleOrDefaultAsync(d => d.DepartmentId == vm.Department.DepartmentId);
            vm.Teacher.Department = department;
            _db.Add(vm.Teacher);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllTeachers");
        }

        //Gestión Asginaturas 
        public async Task<IActionResult> AllSubjects()
        {
            var subjects = await _db.Course.Include(s => s.Teacher).ToListAsync();
            return View(subjects);
        }

        //Add Subject
        public async Task<IActionResult> AddSubject()
        {
            var teacherDisplay = await _db.Teacher.Select(x => new
            {
                Id = x.TeacherId,
                Value = x.TeacherName
            }).ToListAsync();
            SecretaryAddSubjectViewModel vm = new SecretaryAddSubjectViewModel();
            vm.TeacherList = new SelectList(teacherDisplay, "Id", "Value");
            return View(vm);
     
        }
        //Post de addSubject aqui: 

    }
}
