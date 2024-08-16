using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using WebApp.Data;
using WebApp.Models.Domain;
using WebApp.Models.DTO;
using WebApp.Models.DAO;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityDbContext dbContext;

        public StudentsController(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<StudentDto> studentsDto = new List<StudentDto>();
            StudentDAO studentDAO = new StudentDAO(dbContext);
            studentsDto = studentDAO.GetAllStudent();
            return Ok(studentsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var studentDomain = dbContext.Students.Find(id);
            if (studentDomain == null)
            {
                return NotFound();
            }
            StudentDto studentDto = new StudentDto();
            StudentDAO studentDAO = new StudentDAO(dbContext);
            studentDto = studentDAO.GetStudent(studentDomain);
            return Ok(studentDto);
        }

        [HttpPost("SP")]
        public IActionResult CreateByName(AddStudentDto addStudentDto)
        {
            string mySqlTerm = "exec Create_New_Student {0},{1},{2},{3},{4}";
            SqlParameter[] parameters ={
                new SqlParameter("@name",addStudentDto.Name),
                new SqlParameter("@detailId",addStudentDto.DetailId),
                new SqlParameter("@yearId",addStudentDto.YearId),
                new SqlParameter("@branchId",addStudentDto.BranchId),
                new SqlParameter("@type","add"),
            };
            var student = dbContext.Database.ExecuteSqlRaw(mySqlTerm, parameters);

            //dbContext.Students.Add((Student)student);
            //  dbContext.SaveChanges();
            return Ok("success");
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddStudentDto addStudentDto)
        {
            var yearDomain = dbContext.Years.Find(addStudentDto.YearId);
            if (yearDomain == null)
            {
                return NotFound();
            }
            var branchDomain = dbContext.Branches.Find(addStudentDto.BranchId);
            if (branchDomain == null)
            {
                return NotFound();
            }
            var detailDomain = dbContext.Details.Find(addStudentDto.DetailId);
            if (detailDomain == null)
            {
                return NotFound();
            }

            StudentDto studentDto = new StudentDto();
            StudentDAO studentDAO = new StudentDAO(dbContext);
            studentDto = studentDAO.Create(addStudentDto, yearDomain, branchDomain, detailDomain);
            return CreatedAtAction(nameof(GetById), new { id = studentDto.id }, studentDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AddStudentDto addStudentDto)
        {
            var studentDomain = dbContext.Students.Find(id);
            if (studentDomain == null)
            {
                return NotFound();
            }
            var yearDomain = dbContext.Years.Find(addStudentDto.YearId);
            if (yearDomain == null)
            {
                return NotFound();
            }
            var branchDomain = dbContext.Branches.Find(addStudentDto.BranchId);
            if (branchDomain == null)
            {
                return NotFound();
            }
            var detailDomain = dbContext.Details.Find(addStudentDto.DetailId);
            if (detailDomain == null)
            {
                return NotFound();
            }
            StudentDto studentDto = new StudentDto();
            StudentDAO studentDAO = new StudentDAO(dbContext);
            studentDto = studentDAO.Update(studentDomain, addStudentDto, yearDomain, branchDomain, detailDomain);
            return CreatedAtAction(nameof(GetById), new { id = studentDto.id }, studentDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var studentDomain = dbContext.Students.Find(id);
            if (studentDomain == null)
            {
                return NotFound();
            }
            var yearDomain = dbContext.Years.Find(studentDomain.YearId);
            if (yearDomain == null)
            {
                return NotFound();
            }
            var branchDomain = dbContext.Branches.Find(studentDomain.BranchId);
            if (branchDomain == null)
            {
                return NotFound();
            }
            if (yearDomain.Id != 1)
            {
                List<Course> coursesDomain = dbContext.Courses.Where(e => e.BranchId == branchDomain.Id && e.YearId == yearDomain.Id).ToList();
                foreach (Course course in coursesDomain)
                {
                    course.Enrolled--;
                    dbContext.Update(course);
                }
            }
            else if (yearDomain.Id == 1)
            {
                List<Course> coursesDomain = dbContext.Courses.Where(e => e.YearId == yearDomain.Id).ToList();
                foreach (Course course in coursesDomain)
                {
                    course.Enrolled--;
                    dbContext.Update(course);
                }
            }

            yearDomain.Enrolled--;
            branchDomain.Enrolled--;
            dbContext.Remove(studentDomain);
            dbContext.Update(yearDomain);
            dbContext.Update(branchDomain);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}