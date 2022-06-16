using AutoMapper;
using Dal;
using System;

namespace Dto
{
    public class ConstraintsforeigenworkerDto
    {
        public ConstraintsforeigenworkerDto()
        {

        }
        public int confid { get; set; }
        public Nullable<double> age_of_o { get; set; }
        public string languagefw { get; set; }
        public string gender { get; set; }
        public int idcity { get; set; }
        public string addressfg { get; set; }
        public Nullable<int> Level_of_functioningfg { get; set; }
        public Nullable<double> money_of_hour { get; set; }
        public Nullable<long> passwardwor { get; set; }
        public string mail { get; set; }
        public static ConstraintsforeigenworkerDto DalToDto(Constraintsforeigenworker co)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Constraintsforeigenworker, ConstraintsforeigenworkerDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<ConstraintsforeigenworkerDto>(co);
        }
        public Constraintsforeigenworker DtoToDal()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ConstraintsforeigenworkerDto, Constraintsforeigenworker>());
            var mapper = config.CreateMapper();
            return mapper.Map<Constraintsforeigenworker>(this);
        }

    }
}
