using AutoMapper;
using Dal;


namespace Dto
{
    public class CityDto
    {
        public CityDto()
        {

        }
        public int idcity { get; set; }
        public string namecity { get; set; }

        public static CityDto DalToDto(city c)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<city, CityDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<CityDto>(c);
        }
        public city DtoToDal()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CityDto, city>());
            var mapper = config.CreateMapper();
            return mapper.Map<city>(this);
        }
    }
}
