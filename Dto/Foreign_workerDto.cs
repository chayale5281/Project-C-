using AutoMapper;
using Dal;
using System;
namespace Dto
{
    public class Foreign_workerDto
    {
        public Foreign_workerDto()
        {

        }
        public int idwor { get; set; }
        public string lnwor { get; set; }
        public string fnwor { get; set; }
        public int idcity { get; set; }
        public Nullable<int> confid { get; set; }
        public Nullable<bool> ischeck { get; set; }
        //public virtual Type_of_treatment Type_of_treatment { get; set; }
        //public virtual languages languages { get; set; }

        public static Foreign_workerDto DalToDto(Foreign_worker fw)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Foreign_worker, Foreign_workerDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<Foreign_workerDto>(fw);
        }
        public Foreign_worker DtoToDal()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Foreign_workerDto, Foreign_worker>());
            var mapper = config.CreateMapper();
            return mapper.Map<Foreign_worker>(this);
        }
    }
}
