using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.DAO;
using WebApp.Models.DTO;

namespace University.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityDbContext dbContext;

        public CoursesController(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll(){
            List<CourseDto> coursesDto = new List<CourseDto>();
            CourseDAO courseDAO = new CourseDAO(dbContext);
            coursesDto = courseDAO.GetAllCourse();
            return Ok(coursesDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute]int id){
            var courseDomain = dbContext.Courses.Find(id);
            if(courseDomain == null){
                return NotFound();
            }
            CourseDto courseDto = new CourseDto();
            CourseDAO courseDAO = new CourseDAO(dbContext);
            courseDto = courseDAO.GetCourse(courseDomain);
            return Ok(courseDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]AddCourseDto addCourseDto){
          var yearDomain = dbContext.Years.Find(addCourseDto.YearId);
          if(yearDomain == null){
            return NotFound();
          }
          var branchDomain = dbContext.Branches.Find(addCourseDto.BranchId);
          if(branchDomain == null){
            return NotFound();
          }
          CourseDto courseDto = new CourseDto();
          CourseDAO courseDAO = new CourseDAO(dbContext);
          courseDto = courseDAO.Create(addCourseDto,yearDomain,branchDomain);
          return CreatedAtAction(nameof(GetById),new {id=courseDto.Id},courseDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute]int id,[FromBody]AddCourseDto addCourseDto){
            var courseDomain = dbContext.Courses.Find(id);
            if(courseDomain==null){
                return NotFound();
            }
            var yearDomain = dbContext.Years.Find(addCourseDto.YearId);
            if(yearDomain == null){
                return NotFound();
            }
            var branchDomain = dbContext.Branches.Find(addCourseDto.BranchId);
            if(branchDomain == null){
                return NotFound();
            }
            CourseDto courseDto = new CourseDto();
            CourseDAO courseDAO = new CourseDAO(dbContext);
            courseDto = courseDAO.Update(courseDomain,addCourseDto,yearDomain,branchDomain);
            return CreatedAtAction(nameof(GetById),new {id = courseDto.Id},courseDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute]int id){
            var courseDomain = dbContext.Courses.Find(id);
            if(courseDomain == null){
                return NotFound();
            }
            dbContext.Remove(courseDomain);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}