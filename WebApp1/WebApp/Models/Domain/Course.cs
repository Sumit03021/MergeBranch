
using System.ComponentModel.DataAnnotations;


namespace WebApp.Models.Domain
{
    public class Course
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