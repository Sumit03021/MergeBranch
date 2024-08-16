using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.DAO;
using WebApp.Models.DTO;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetailsController : ControllerBase
    {
        private readonly UniversityDbContext dbContext;

        public DetailsController(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<DetailDto> detailsDto = new List<DetailDto>();
            DetailDAO detailDAO = new DetailDAO(dbContext);
            detailsDto = detailDAO.GetAllDetail();
            return Ok(detailsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var detailDomain = dbContext.Details.Find(id);
            if (detailDomain == null)
            {
                return NotFound();
            }
            DetailDto detailDto = new DetailDto();
            DetailDAO detailDAO = new DetailDAO(dbContext);
            detailDto = detailDAO.GetDetail(detailDomain);
            return Ok(detailDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddDetailDto addDetailDto)
        {
            DetailDto detailDto = new DetailDto();
            DetailDAO detailDAO = new DetailDAO(dbContext);
            detailDto = detailDAO.Create(addDetailDto);
            return CreatedAtAction(nameof(GetById), new { id = detailDto.Id }, detailDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AddDetailDto addDetailDto)
        {
            var detailDomain = dbContext.Details.Find(id);
            if (detailDomain == null)
            {
                return NotFound();
            }
            DetailDto detailDto = new DetailDto();
            DetailDAO detailDAO = new DetailDAO(dbContext);
            detailDto = detailDAO.Update(detailDomain, addDetailDto);
            return CreatedAtAction(nameof(GetById), new { id = detailDto.Id }, detailDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var detailDomain = dbContext.Details.Find(id);
            if (detailDomain == null)
            {
                return NotFound();
            }
            dbContext.Remove(detailDomain);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}