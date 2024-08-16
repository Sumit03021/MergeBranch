
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models.Domain;
using WebApp.Models.DTO;

namespace WebApp.Models.DAO
{
    public class CourseDAO
    {
        private readonly UniversityDbContext dbContext;

        public CourseDAO(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CourseDto> GetAllCourse(){
            List<Course> coursesDomain = dbContext.Courses.Include(t => t.Year).Include(t => t.Branch).ToList();
            List<CourseDto> coursesDto = new List<CourseDto>();
            foreach(var course in coursesDomain){
               coursesDto.Add(new CourseDto(){
                Id = course.Id,
                Name = course.Name,
                Enrolled = course.Enrolled,
                YearId = course.YearId,
                BranchId = course.BranchId,
                Year = course.Year,
                Branch = course.Branch,
               });
            }
            return coursesDto;
        }

        public CourseDto GetCourse(Course courseDomain){
            CourseDto courseDto = new CourseDto(){
                Id = courseDomain.Id,
                Name = courseDomain.Name,
                Enrolled = courseDomain.Enrolled,
                Year = courseDomain.Year,
                Branch = courseDomain.Branch,
                YearId = courseDomain.YearId,
                BranchId = courseDomain.BranchId
            };
            return courseDto;
        }

        public CourseDto Create(AddCourseDto addCourseDto,Year yearDomain, Branch branchDomain){
            var courseDomain = new Course();
            courseDomain.Name = addCourseDto.Name;
            courseDomain.Enrolled = addCourseDto.Enrolled;
            courseDomain.YearId = yearDomain.Id;
            courseDomain.BranchId = branchDomain.Id;
            courseDomain.Year = yearDomain;
            courseDomain.Branch = branchDomain;
            dbContext.Add(courseDomain);
            dbContext.SaveChanges();
            CourseDto courseDto = new CourseDto(){
                Id = courseDomain.Id,
                Name = courseDomain.Name,
                Enrolled = courseDomain.Enrolled,
                YearId = courseDomain.YearId,
                BranchId = courseDomain.BranchId,
                Year = courseDomain.Year,
                Branch = courseDomain.Branch
            };
            return courseDto;
        }
        public CourseDto Update(Course courseDomain , AddCourseDto addCourseDto,Year yearDomain, Branch branchDomain){
            courseDomain.Name = addCourseDto.Name;
            courseDomain.Enrolled = addCourseDto.Enrolled;
            courseDomain.YearId = yearDomain.Id;
            courseDomain.BranchId = branchDomain.Id;
            courseDomain.Year = yearDomain;
            courseDomain.Branch = branchDomain;
            dbContext.Add(courseDomain);
            dbContext.SaveChanges();
            CourseDto courseDto = new CourseDto(){
                Id = courseDomain.Id,
                Name = courseDomain.Name,
                Enrolled = courseDomain.Enrolled,
                YearId = courseDomain.YearId,
                BranchId = courseDomain.BranchId,
                Year = courseDomain.Year,
                Branch = courseDomain.Branch
            };
            return courseDto;
        }
    }
}