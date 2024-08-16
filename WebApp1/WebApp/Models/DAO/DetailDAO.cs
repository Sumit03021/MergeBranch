using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models.Domain;
using WebApp.Models.DTO;

namespace WebApp.Models.DAO
{
    public class DetailDAO
    {
        private readonly UniversityDbContext dbContext;

        public DetailDAO(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

         public List<DetailDto> GetAllDetail(){
            List<Detail> detailsDomain = dbContext.Details.ToList();
            List<DetailDto> detailsDto = new List<DetailDto>();
            foreach(var detail in detailsDomain){
               detailsDto.Add(new DetailDto(){
                Id = detail.Id,
                Mobile = detail.Mobile,
                District = detail.District,
                City = detail.City,
                Pincode = detail.Pincode,
                Country = detail.Country,
               });
            }
            return detailsDto;
        }

        public DetailDto GetDetail(Detail detailDomain){
            DetailDto detailDto = new DetailDto(){
                Id = detailDomain.Id,
                Mobile = detailDomain.Mobile,
                District = detailDomain.District,
                City = detailDomain.City,
                Pincode = detailDomain.Pincode,
                Country = detailDomain.Country,
            };
            return detailDto;
        }

        public DetailDto Create(AddDetailDto addDetailDto){
            var detailDomain = new Detail();
            detailDomain.Mobile = addDetailDto.Mobile;
            detailDomain.District = addDetailDto.District;
            detailDomain.City = addDetailDto.City;
            detailDomain.Pincode = addDetailDto.Pincode;
            detailDomain.Country = addDetailDto.Country;

            dbContext.Add(detailDomain);
            dbContext.SaveChanges();

            DetailDto detailDto = new DetailDto(){
                Id = detailDomain.Id,
                Mobile = detailDomain.Mobile,
                District = detailDomain.District,
                City = detailDomain.City,
                Pincode = detailDomain.Pincode,
                Country = detailDomain.Country,
            };
            return detailDto;
        }
        public DetailDto Update(Detail detailDomain , AddDetailDto addDetailDto){
            detailDomain.Mobile = addDetailDto.Mobile;
            detailDomain.District = addDetailDto.District;
            detailDomain.City = addDetailDto.City;
            detailDomain.Pincode = addDetailDto.Pincode;
            detailDomain.Country = addDetailDto.Country;
        
            dbContext.Add(detailDomain);
            dbContext.SaveChanges();
            DetailDto yearDto = new DetailDto(){
                Id = detailDomain.Id,
                Mobile = detailDomain.Mobile,
                District = detailDomain.District,
                City = detailDomain.City,
                Pincode = detailDomain.Pincode,
                Country = detailDomain.Country,
            };
            return yearDto;
        }
    }
}