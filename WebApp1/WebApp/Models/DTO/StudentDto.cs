using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Domain;

namespace WebApp.Models.DTO
{
    public class StudentDto
    {
        public int id{get;set;}
        [Required]
        public string Name{get;set;}
        public string Rollno{get;set;}
        [Required]
        public int YearId{get;set;}
        [Required]
        public int BranchId{get;set;}
        [Required]
        public int DetailId{get;set;}
        public Year Year{get;set;}
        public Branch Branch { get; set; }
        public Detail Detail { get; set; }
    }
}