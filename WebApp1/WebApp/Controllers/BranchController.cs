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
    public class BranchesController : ControllerBase
    {
        private readonly UniversityDbContext dbContext;

        public BranchesController(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<BranchDto> branchesDto = new List<BranchDto>();
            BranchDAO branchDAO = new BranchDAO(dbContext);
            branchesDto = branchDAO.GetAllBranch();
            return Ok(branchesDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var branchDomain = dbContext.Branches.Find(id);
            if (branchDomain == null)
            {
                return NotFound();
            }
            BranchDto branchDto = new BranchDto();
            BranchDAO branchDAO = new BranchDAO(dbContext);
            branchDto = branchDAO.GetBranch(branchDomain);
            return Ok(branchDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddBranchDto addBranchDto)
        {
            BranchDto branchDto = new BranchDto();
            BranchDAO branchDAO = new BranchDAO(dbContext);
            branchDto = branchDAO.Create(addBranchDto);
            return CreatedAtAction(nameof(GetById), new { id = branchDto.Id }, branchDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AddBranchDto addBranchDto)
        {
            var branchDomain = dbContext.Branches.Find(id);
            if (branchDomain == null)
            {
                return NotFound();
            }
            BranchDto branchDto = new BranchDto();
            BranchDAO branchDAO = new BranchDAO(dbContext);
            branchDto = branchDAO.Update(branchDomain, addBranchDto);
            return CreatedAtAction(nameof(GetById), new { id = branchDto.Id }, branchDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var branchDomain = dbContext.Branches.Find(id);
            if (branchDomain == null)
            {
                return NotFound();
            }
            dbContext.Remove(branchDomain);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}