using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace WebApp.Models.Domain
{
    public class Detail
    {
        public int Id{get;set;}
        [Required]
        public string Mobile{get;set;}
        [Required]
        public string District{get;set;}
        [Required]
        public string City { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}