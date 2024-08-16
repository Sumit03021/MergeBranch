using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Domain
{
    public class Branch
    {
        public int Id{get;set;}
        [Required]
        public string Name{get;set;}
        public int Enrolled{get;set;}
        [Required]
        public string Code{get;set;}
    }
}