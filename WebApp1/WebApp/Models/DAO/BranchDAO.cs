using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models.Domain;
using WebApp.Models.DTO;

namespace WebApp.Models.DAO
{
    public class BranchDAO
    {
        private readonly UniversityDbContext dbContext;
        public BranchDAO(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<BranchDto> GetAllBranch(){
            List<Branch> branchesDomain = dbContext.Branches.ToList();
            List<BranchDto> branchesDto = new List<BranchDto>();
            foreach(var branch in branchesDomain){
               branchesDto.Add(new BranchDto(){
                Id = branch.Id,
                Name = branch.Name,
                Enrolled = branch.Enrolled,
                Code = branch.Code,
               });
            }
            return branchesDto;
        }

        public BranchDto GetBranch(Branch branchDomain){
            BranchDto branchDto = new BranchDto(){
                Id = branchDomain.Id,
                Name = branchDomain.Name,
                Enrolled = branchDomain.Enrolled,
                Code = branchDomain.Code,
            };
            return branchDto;
        }

        public BranchDto Create(AddBranchDto addBranchDto){
            var branchDomain = new Branch();
            branchDomain.Name = addBranchDto.Name;
            branchDomain.Enrolled = addBranchDto.Enrolled;
            branchDomain.Code = addBranchDto.Code;
            dbContext.Add(branchDomain);
            dbContext.SaveChanges();
            BranchDto branchDto = new BranchDto(){
                Id = branchDomain.Id,
                Name = branchDomain.Name,
                Enrolled = branchDomain.Enrolled,
                Code =   branchDomain.Code,
            };
            return branchDto;
        }

        public BranchDto Update(Branch branchDomain , AddBranchDto addBranchDto){
            branchDomain.Name = addBranchDto.Name;
            branchDomain.Enrolled = addBranchDto.Enrolled;
            branchDomain.Code = addBranchDto.Code;
        
            dbContext.Add(branchDomain);
            dbContext.SaveChanges();
            BranchDto branchDto = new BranchDto(){
                Id = branchDomain.Id,
                Name = branchDomain.Name,
                Enrolled = branchDomain.Enrolled,
                Code = branchDomain.Code,
            };
            return branchDto;
        }
    }
}