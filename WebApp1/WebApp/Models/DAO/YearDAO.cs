using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models.Domain;
using WebApp.Models.DTO;



namespace WebApp.Models.DAO
{
    public class YearDAO
    {
        private readonly UniversityDbContext dbContext;

        public YearDAO(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<YearDto> GetAllYear(){
            List<Year> yearsDomain = dbContext.Years.ToList();
            List<YearDto> yearsDto = new List<YearDto>();
            foreach(var year in yearsDomain){
               yearsDto.Add(new YearDto(){
                Id = year.Id,
                Name = year.Name,
                Enrolled = year.Enrolled,
               });
            }
            return yearsDto;
        }

        public YearDto GetYear(Year yearDomain){
            YearDto yearDto = new YearDto(){
                Id = yearDomain.Id,
                Name = yearDomain.Name,
                Enrolled = yearDomain.Enrolled
            };
            return yearDto;
        }

        public YearDto Create(AddYearDto addYearDto){
            var yearDomain = new Year();
            yearDomain.Name = addYearDto.Name;
            yearDomain.Enrolled = addYearDto.Enrolled;
            dbContext.Add(yearDomain);
            dbContext.SaveChanges();
            YearDto yearDto = new YearDto(){
                Id = yearDomain.Id,
                Name = yearDomain.Name,
                Enrolled = yearDomain.Enrolled
            };
            return yearDto;
        }
        public YearDto Update(Year yearDomain , AddYearDto addYearDto){
            yearDomain.Name = addYearDto.Name;
            yearDomain.Enrolled = addYearDto.Enrolled;
        
            dbContext.Add(yearDomain);
            dbContext.SaveChanges();
            YearDto yearDto = new YearDto(){
                Id = yearDomain.Id,
                Name = yearDomain.Name,
                Enrolled = yearDomain.Enrolled
            };
            return yearDto;
        }
    }
}