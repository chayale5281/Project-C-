using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using AutoMapper;
namespace Dto
{
   public class Days_of_wor2Dto
    {
            public Days_of_wor2Dto()
            {

            }
            public int iddayw { get; set; }
            public Nullable<int> idwor { get; set; }
            //public Nullable<bool> isSunday { get; set; }
            //public Nullable<bool> isManday { get; set; }
            //public Nullable<bool> isTuthday { get; set; }
            //public Nullable<bool> isWenthday { get; set; }
            //public Nullable<bool> isThursday { get; set; }
            //public Nullable<bool> isFriday { get; set; }
            //public Nullable<bool> isShabbat { get; set; }
            public Nullable<int> hourstartw { get; set; }
            public Nullable<int> hourendw { get; set; }
            public Nullable<int> confid { get; set; }
            public Nullable<bool> isavliable { get; set; }


        public bool[] days { get; set; } = new bool[7];
        public static Days_of_wor2Dto DalToDto(days_of_wor2 d)
        {
            return new Days_of_wor2Dto()
            {
                iddayw = d.iddayw,
                idwor = d.idwor,
                days = new bool[] { d.isSunday.Value, d.isManday.Value, d.isThursday.Value, d.isWenthday.Value, d.isThursday.Value, d.isFriday.Value, d.isShabbat.Value },
                hourstartw = d.hourstartw,
                hourendw = d.hourendw,
                confid = d.confid,
                isavliable = d.isavliable
            };
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<days_of_wor2, Days_of_wor2Dto>());
            //    var mapper = config.CreateMapper();
            //    return mapper.Map<Days_of_wor2Dto>(d);
            //}
        }
            public days_of_wor2 DtoToDal()
            {
            days_of_wor2 d = new days_of_wor2();
            d.iddayw = this.iddayw;
            d.idwor = this.idwor;
            d.isSunday = this.days[0];
            d.isManday = this.days[1];
            d.isTuthday = this.days[2];
            d.isWenthday = this.days[3];
            d.isThursday = this.days[4];
            d.isFriday = this.days[5];
            d.isShabbat = this.days[6];
            return d;
                //var config = new MapperConfiguration(cfg => cfg.CreateMap<Days_of_wor2Dto, days_of_wor2>());
                //var mapper = config.CreateMapper();
                //return mapper.Map<days_of_wor2>(this);
            }
        }
    }

