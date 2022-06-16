using AutoMapper;
using Dal;
using System;
namespace Dto
{
    public class OldDto
    {
        public OldDto()
        {

        }
        public int idold { get; set; }
        public string fnameold { get; set; }
        public string lnameold { get; set; }
        public int idcity { get; set; }
        public Nullable<int> nomhomeold { get; set; }
        public string nationold { get; set; }
        public Nullable<int> conoID { get; set; }
        public Nullable<bool> ischeck { get; set; }

        //public virtual Type_of_treatment Type_of_treatment { get; set; }
        //public virtual languages languages { get; set; }
        // public virtual city city { get; set; }

        public static OldDto DalToDto(old o)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<old, OldDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<OldDto>(o);
        }
        public old DtoToDal()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OldDto, old>());
            var mapper = config.CreateMapper();
            return mapper.Map<old>(this);
        }
    }
}
