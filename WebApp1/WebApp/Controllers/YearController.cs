using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.DAO;
using WebApp.Models.DTO;

namespace University.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearsController : ControllerBase
    {
        private readonly UniversityDbContext dbContext;

        public YearsController(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll(){
         List<YearDto> yearsDto = new List<YearDto>();
         YearDAO yearDAO = new YearDAO(dbContext);
         yearsDto = yearDAO.GetAllYear();
         return Ok(yearsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute]int id){
            var yearDomain = dbContext.Years.Find(id);
            if(yearDomain == null){
                return NotFound();
            }
            YearDto yearDto = new YearDto();
            YearDAO yearDAO = new YearDAO(dbContext);
            yearDto = yearDAO.GetYear(yearDomain);
            return Ok(yearDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddYearDto addYearDto){
            YearDto yearDto = new YearDto();
            YearDAO yearDAO = new YearDAO(dbContext);
            yearDto = yearDAO.Create(addYearDto);
            return CreatedAtAction(nameof(GetById),new {id = yearDto.Id},yearDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute]int id,[FromBody] AddYearDto addYearDto){
            var yearDomain = dbContext.Years.Find(id);
            if(yearDomain == null){
                return NotFound();
            }
            YearDto yearDto = new YearDto();
            YearDAO yearDAO = new YearDAO(dbContext);
            yearDto = yearDAO.Update(yearDomain,addYearDto);
            return CreatedAtAction(nameof(GetById),new {id= yearDto.Id},yearDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id){
            var yearDomain = dbContext.Years.Find(id);
            if(yearDomain == null){
                return NotFound();
            }
            dbContext.Remove(yearDomain);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}