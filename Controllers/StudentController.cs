using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles ="Student,Admin,Secretary")]
    public class StudentController:Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProfile()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Student student = new Student();
            if(_db.Students.Any(i=>i.UserId == currentUserId))
            {
                student = _db.Students.FirstOrDefault(i => i.UserId == currentUserId);
            }
            else
            {
                student.UserId = currentUserId;
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile(Student student)
        {
            var stdnt = _db.Students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if(stdnt != null)
            {
                stdnt.StudentName = student.StudentName;
                stdnt.StudentDNI = student.StudentDNI;
                stdnt.StudentAge = student.StudentAge;
                stdnt.StudentAddress = student.StudentAddress;
                stdnt.StudentEmail = student.StudentEmail;
            }
            else
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                student.UserId = currentUserId;
                _db.Add(student);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AllProfiles()
        {
            var students = _db.Students.ToList();
            return View(students);
        }


        public async Task<IActionResult> EnrollCourse()
        {
            var studentDisplay = await _db.Students.Select(x => new
            {
                Id = x.StudentId,
                Value=x.StudentName
            }).ToListAsync();

            var courseDisplay = await _db.Course.Select(x => new
            {
                Id=x.CourseId,
                Value=x.CourseName
            }).ToListAsync();

            StudentEnrrollCourseViewModel vm = new StudentEnrrollCourseViewModel();
            vm.StudentList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(studentDisplay,"Id","Value");
            vm.CourseList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(courseDisplay, "Id", "Value");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollCourse(StudentEnrrollCourseViewModel vm)
        {
            //hacer post
        }

        public async Task<IActionResult> AllSubjects()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var studentId = _db.Students.FirstOrDefault(s => s.UserId == currentUserId).StudentId;
            //var subjects = await _db.Enrollments.Where(x => x.StudentId == studentId).ToListAsync();
            Enrollment enrollment = await _db.Enrollments.SingleOrDefaultAsync(x => x.StudentId == studentId);
            return View(enrollment);
        }
     
    }
}
