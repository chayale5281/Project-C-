using Dal;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Bll
{
    public class Days_of_oldbll
    {
        public RequestResult GetAllDaysOld()
        {
            using (dbEntities db = new dbEntities())

            {
                List<Days_of_oldDto> lst = new List<Days_of_oldDto>();
                foreach (var item in db.days_of_old2.ToList())
                {
                    lst.Add(Days_of_oldDto.DalToDto(item));
                }
                return new RequestResult()
                {
                    Data = lst,
                    Message = "succesfull",
                    status = true
                };
            }
        }

        public void AddDaysOld(Days_of_oldDto day )
        {

            
            
            using (dbEntities db = new dbEntities())
            {
                db.days_of_old2.Add(day.DtoToDal());
                db.SaveChanges();
            }
        }
        public Days_of_oldDto Login(ConstraintsoldDto c)
        {
            using (dbEntities db = new dbEntities())
            {
                Days_of_oldDto co = Days_of_oldDto.DalToDto(db.days_of_old2.FirstOrDefault(d => d.conoID == c.conoID));


                return co;

            }
        }
        public void Update(int idday, Days_of_oldDto d)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (days_of_old2 day in db.days_of_old2)
                {
                    if (day.iddayo == idday)
                    {
                        day.iddayo = d.iddayo;
                        day.idold = d.idold;
                        day.isSunday = d.days[0];
                        day.isManday = d.days[1];
                        day.isThursday = d.days[2];
                        day.isWenthday = d.days[3];
                        day.isTuthday = d.days[4];
                        day.isFriday = d.days[5];
                        day.isShabbat = d.days[6];
                        day.conoID = d.conoID;
                        day.hourstarto = d.hourstarto;
                        day.hourendo = d.hourendo;
                    }

                }
                db.SaveChanges();

            }
        }
        public bool []switchs(string[] days)
        {
            string[] dayes = { "שבת", "שישי", "חמישי", "רביעי", "שלישי", "שני", "ראשון" };
            bool[] dayss = { false,false, false, false, false, false, false };
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (dayes[i].Equals(days[j])) { 
                        dayss[i] = true;
                        break;
                    }
                      
                }
            }

            return dayss;
        }

    
}
}
