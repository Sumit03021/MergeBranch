using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Domain;
using WebApp.Models.DTO;

namespace WebApp.Models.DAO
{
    public class StudentDAO
    {
        private readonly UniversityDbContext dbContext;

        public StudentDAO(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

         public List<StudentDto> GetAllStudent(){
            List<Student> studentsDomain = dbContext.Students.Include(t =>t.Year).Include(t =>t.Branch).Include(t =>t.Detail).ToList();
            List<StudentDto> studentsDto = new List<StudentDto>();
            foreach(var student in studentsDomain){
               studentsDto.Add(new StudentDto(){
                id = student.id,
                Name = student.Name,
                Rollno = student.Rollno,
                YearId = student.YearId,
                BranchId = student.BranchId,
                DetailId = student.DetailId,
                Year = student.Year,
                Branch = student.Branch,
                Detail = student.Detail,
               });
            }
            return studentsDto;
        }

        public StudentDto GetStudent(Student studentDomain){
            StudentDto studentDto = new StudentDto(){
                id = studentDomain.id,
                Name = studentDomain.Name,
                Rollno = studentDomain.Rollno,
                YearId = studentDomain.YearId,
                BranchId = studentDomain.BranchId,
                DetailId = studentDomain.DetailId,
                Year = studentDomain.Year,
                Branch = studentDomain.Branch,
                Detail = studentDomain.Detail,
            };
            return studentDto;
        }

        public StudentDto Create(AddStudentDto addStudentDto,Year yearDomain,Branch branchDomain , Detail detailDomain){
            var studentDomain = new Student();
            studentDomain.Name = addStudentDto.Name;
            studentDomain.Rollno = addStudentDto.Rollno;
            studentDomain.YearId = yearDomain.Id;
            studentDomain.BranchId = branchDomain.Id;
            studentDomain.DetailId = detailDomain.Id;
            studentDomain.Detail = detailDomain;
            studentDomain.Branch = branchDomain;
            studentDomain.Year = yearDomain;
            yearDomain.Enrolled++;
            branchDomain.Enrolled++;
            if(yearDomain.Id != 1){
            List<Course> coursesDomain = dbContext.Courses.Where(e => e.BranchId == addStudentDto.BranchId && e.YearId == addStudentDto.YearId).ToList();
            foreach(Course course in coursesDomain){
                course.Enrolled++;
                dbContext.Update(course);
            }
            }else if(yearDomain.Id == 1){
                List<Course> coursesDomain = dbContext.Courses.Where(e => e.YearId == addStudentDto.YearId).ToList();
            foreach(Course course in coursesDomain){
                course.Enrolled++;
                dbContext.Update(course);
            }
            }
            
            dbContext.Add(studentDomain);
            dbContext.Update(yearDomain);
            dbContext.Update(branchDomain);
            dbContext.SaveChanges();
            StudentDto studentDto = new StudentDto(){
                id = studentDomain.id,
                Name = studentDomain.Name,
                Rollno = studentDomain.Rollno,
                Year = studentDomain.Year,
                Branch = studentDomain.Branch,
                Detail = studentDomain.Detail,
                YearId = studentDomain.YearId,
                BranchId = studentDomain.BranchId,
                DetailId = studentDomain.DetailId,
            };
            return studentDto;
        }
        public StudentDto Update(Student studentDomain , AddStudentDto addStudentDto,Year yearDomain , Branch branchDomain , Detail detailDomain){
            Year oldYearDomain = studentDomain.Year;
            Branch oldBranchDomain = studentDomain.Branch;
            if(oldYearDomain.Id != 1){
             List<Course> oldCoursesDomain = dbContext.Courses.Where(e => e.BranchId == oldBranchDomain.Id && e.YearId == oldYearDomain.Id).ToList();
            foreach(Course course in oldCoursesDomain){
                course.Enrolled--;
                dbContext.Update(course);
            }
            }else if(oldYearDomain.Id == 1){
                List<Course> oldCoursesDomain = dbContext.Courses.Where(e => e.YearId == oldYearDomain.Id).ToList();
            foreach(Course course in oldCoursesDomain){
                course.Enrolled--;
                dbContext.Update(course);
            }
            }
            
            oldYearDomain.Enrolled--;
            oldBranchDomain.Enrolled--;
            studentDomain.Name = addStudentDto.Name;
            studentDomain.Rollno = addStudentDto.Rollno;
            studentDomain.YearId = yearDomain.Id;
            studentDomain.BranchId = branchDomain.Id;
            studentDomain.DetailId = detailDomain.Id;
            studentDomain.Detail = detailDomain;
            studentDomain.Branch = branchDomain;
            studentDomain.Year = yearDomain;
            if(yearDomain.Id !=1){
             List<Course> coursesDomain = dbContext.Courses.Where(e => e.BranchId == addStudentDto.BranchId && e.YearId == addStudentDto.YearId).ToList();
            foreach(Course course in coursesDomain){
                course.Enrolled++;
                dbContext.Update(course);
            }
            }else if(yearDomain.Id == 1){
                List<Course> coursesDomain = dbContext.Courses.Where(e => e.YearId == addStudentDto.YearId).ToList();
            foreach(Course course in coursesDomain){
                course.Enrolled++;
                dbContext.Update(course);
            }
            }
            
            yearDomain.Enrolled++;
            branchDomain.Enrolled++;
            dbContext.Add(studentDomain);
            dbContext.Update(oldYearDomain);
            dbContext.Update(oldBranchDomain);
            dbContext.Update(yearDomain);
            dbContext.Update(branchDomain);
            dbContext.SaveChanges();
            StudentDto studentDto = new StudentDto(){
                id = studentDomain.id,
                Name = studentDomain.Name,
               Rollno = studentDomain.Rollno,
                Year = studentDomain.Year,
                Branch = studentDomain.Branch,
                Detail = studentDomain.Detail,
                YearId = studentDomain.YearId,
                BranchId = studentDomain.BranchId,
                DetailId = studentDomain.DetailId,
            };
            return studentDto;
        }

        // public int CreateStudent(AddStudentDto addStudentDto){
        //     int result = 0;
        //     string Name = addStudentDto.Name;
        //     int BranchId = addStudentDto.BranchId;
        //     int YearId = addStudentDto.YearId;
        //     int DetailId = addStudentDto.DetailId;
        //     string mySqlTerm = "exec Create_New_Student @name,@detailId,@yearId,@branchId,@type";
        //     SqlParameter []parameters ={
        //         new SqlParameter("@name",Name),
        //         new SqlParameter("@detailId",DetailId),
        //         new SqlParameter("@yearId",YearId),
        //         new SqlParameter("@branchId",BranchId),
        //         new SqlParameter("@type","add"),
        //     };
        //     var student = dbContext.Students.FromSqlInterpolated(mySqlTerm,parameters)
        //     if(student == null){
        //         return result=0;
        //     }
        //     return result=1;
        // }
    }
}