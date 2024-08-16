using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Domain;

namespace WebApp.Models.DTO
{
    public class CourseDto
    {
        public int Id{get;set;}
        [Required]
        public string Name { get; set; }
        public int Enrolled{get;set;}
        public int YearId{get;set;}
        public int BranchId{get;set;}
        [Required]
        public Year Year{get;set;}
        [Required]
        public Branch Branch{get;set;}
    }
}