using AutoMapper;
using Dal;
using System;

namespace Dto
{
    public class Days_of_oldDto
    {
        public Days_of_oldDto()
        {

        }
        public int iddayo { get; set; }
        public Nullable<int> idold { get; set; }
        //public Nullable<bool> isSunday { get; set; }
        //public Nullable<bool> isManday { get; set; }
        //public Nullable<bool> isTuthday { get; set; }
        //public Nullable<bool> isWenthday { get; set; }
        //public Nullable<bool> isThursday { get; set; }
        //public Nullable<bool> isFriday { get; set; }
        //public Nullable<bool> isShabbat { get; set; }
        public Nullable<int> hourstarto { get; set; }
        public Nullable<int> hourendo { get; set; }
        public Nullable<int> conoID { get; set; }
        public Nullable<bool> isavliable { get; set; }

        public bool[] days { get; set; } = new bool[7];

        public static Days_of_oldDto DalToDto(days_of_old2 d)
        {
            return new Days_of_oldDto()
            {
                iddayo = d.iddayo,
                idold = d.idold,
                days = new bool[] { d.isSunday.Value, d.isManday.Value, d.isThursday.Value, d.isWenthday.Value, d.isThursday.Value, d.isFriday.Value, d.isShabbat.Value },
                hourstarto = d.hourstarto,
                hourendo = d.hourendo,
                conoID=d.conoID,
                isavliable=d.isavliable
            };

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<days_of_old2, Days_of_oldDto>());
            //var mapper = config.CreateMapper();
            //return mapper.Map<Days_of_oldDto>(d);
        }
        public days_of_old2 DtoToDal()
        {
           days_of_old2 d= new days_of_old2();
            d.iddayo = this.iddayo;
            d.idold = this.idold;
            d.isSunday = this.days[0];
            d.isManday = this.days[1];
            d.isTuthday = this.days[2];
            d.isWenthday = this.days[3];
            d.isThursday = this.days[4];
            d.isFriday = this.days[5];
            d.isShabbat = this.days[6];
            return d;
            // המרה מפורשת...
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Days_of_oldDto, days_of_old2>());
            //var mapper = config.CreateMapper();
            //return mapper.Map<days_of_old2>(this);
        }
    }
}





