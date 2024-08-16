using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.DTO
{
    public class YearDto
    {
        public int Id{get;set;}
        [Required]
        public string Name { get; set; }
        public int Enrolled{get;set;}
    }
}