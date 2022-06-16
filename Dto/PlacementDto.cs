using AutoMapper;
using Dal;
using System;

namespace Dto
{
    public class PlacementDto
    {
        public PlacementDto()
        {

        }
        public int idPlacement { get; set; }
        public int idold { get; set; }
        public int idwor { get; set; }
        public Nullable<System.DateTime> Start_Datep { get; set; }
        public Nullable<System.DateTime> end_Datep { get; set; }
        public string nameold { get; set; }
        public string namewor { get; set; }
        public string dayes { get; set; }
        public static PlacementDto DalToDto(Placement p)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Placement, PlacementDto>());
            var mapper = config.CreateMapper();
            return mapper.Map<PlacementDto>(p);
        }
        public Placement DtoToDal()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PlacementDto, Placement>());
            var mapper = config.CreateMapper();
            return mapper.Map<Placement>(this);
        }
    }
}
