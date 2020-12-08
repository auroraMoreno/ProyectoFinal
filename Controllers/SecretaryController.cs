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

        //Modificar departamento
        public IActionResult EditDepartment(int id)
        {
            Department department;
            department = _db.Department.Find(id);
            return View(department);
        }

        [HttpPost]
        public IActionResult EditDepartment(Department department)
        {
            _db.Update(department);
            _db.SaveChanges();
            return RedirectToAction("AllDepartments");

        }

        //Delete departamento
        public IActionResult DeleteDepartment(int id)
        {
            Department department;
            department = _db.Department.Find(id);
            return View(department);

        }

        [HttpPost]
        public IActionResult DeleteDepartment(Department department)
        {
            _db.Remove(department);
            _db.SaveChanges();
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

        //Modificar profesor 
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacherToUpdate = _db.Teacher.FirstOrDefault(t => t.TeacherId == id);
            SecretaryEditTeacherViewModel vm = new SecretaryEditTeacherViewModel();
            vm.Teacher = teacherToUpdate;
            var departmentDisplay = await _db.Department.Select(x => new
            {
                Id=x.DepartmentId,
                Value=x.DepartmentName

            }).ToListAsync();
            vm.DepartmentList = new SelectList(departmentDisplay, "Id", "Value");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditTeacher(SecretaryEditTeacherViewModel vm)
        {
            var department = await _db.Department.SingleOrDefaultAsync(d => d.DepartmentId == vm.Department.DepartmentId);
            vm.Teacher.Department = department;
            _db.Update(vm.Teacher);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllTeachers");
        }

        //Eliminar profesor
        public IActionResult DeleteTeacher(int id)
        {
            SecretaryDeleteTeacherViewModel vm = new SecretaryDeleteTeacherViewModel();
            vm.Teacher = _db.Teacher.Find(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeacher(SecretaryDeleteTeacherViewModel vm)
        {
            _db.Remove(vm.Teacher);
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
        [HttpPost]
        public async Task<IActionResult> AddSubject(SecretaryAddSubjectViewModel vm)
        {
            var teacher = await _db.Teacher.SingleOrDefaultAsync(t => t.TeacherId == vm.Teacher.TeacherId);
            vm.Course.Teacher = teacher;
            _db.Add(vm.Course);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllSubjects");
        }

        //Modificar asignatura
        public async Task<IActionResult> EditSubject(int id)
        {
            var subjectToUpdate = _db.Course.FirstOrDefault(s => s.CourseId == id);
            SecretaryEditSubjectViewModel vm = new SecretaryEditSubjectViewModel();
            vm.Course = subjectToUpdate;
            var teacherDisplay = await _db.Teacher.Select(x => new
            {
                Id=x.TeacherId,
                Value = x.TeacherName

            }).ToListAsync();
            vm.TeacherList = new SelectList(teacherDisplay, "Id", "Value");
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> EditSubject(SecretaryEditSubjectViewModel vm)
        {
            var teacher = await _db.Teacher.SingleOrDefaultAsync(t => t.TeacherId == vm.Teacher.TeacherId);
            vm.Course.Teacher = teacher;
            _db.Update(vm.Course);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllSubjects");
        }

        //Delete

        public IActionResult DeleteSubject(int id)
        {
            SecretaryDeleteSubjectViewModel vm = new SecretaryDeleteSubjectViewModel();
            vm.Course = _db.Course.Find(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubject(SecretaryDeleteSubjectViewModel vm)
        {
            _db.Remove(vm.Course);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllSubjects");
        }

        //incidents
        public async Task<IActionResult> AllIncidents()
        {
            var incidents = await _db.Incidents.Include(i => i.Teacher).ToListAsync();
            return View(incidents);
        }

    }
}
