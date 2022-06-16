using AutoMapper;
using Dal;
using System;

namespace Dto
{
    public class ConstraintsoldDto
    {
        public ConstraintsoldDto()
        {

        }
        public int conoID { get; set; }
        public Nullable<double> age { get; set; }
        public Nullable<int> Level_of_functioningo { get; set; }
        public string gender { get; set; }
        public int idcity { get; set; }
        public string addressold { get; set; }
        public string languageold { get; set; }
        public Nullable<double> hanacha_and_money_for_hour { get; set; }
        public Nullable<long> passwardold { get; set; }
        public string mail { get; set; }

        public static ConstraintsoldDto DalToDto(Constraintsold co)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Constraintsold, ConstraintsoldDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<ConstraintsoldDto>(co);
        }
        public Constraintsold DtoToDal()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ConstraintsoldDto, Constraintsold>());
            var mapper = config.CreateMapper();
            return mapper.Map<Constraintsold>(this);
        }

    }
}
