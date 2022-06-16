using Dal;
using System.Collections.Generic;
using System.Linq;
using Dto;
namespace Bll
{
    public class Daysofworbll
    {
        public RequestResult GetAllDaysWor()
        {
            using (dbEntities db = new dbEntities())
            {
                List<Days_of_wor2Dto> lst = new List<Days_of_wor2Dto>();
                foreach (var item in db.days_of_wor2.ToList())
                    lst.Add(Days_of_wor2Dto.DalToDto(item));
                return new RequestResult()
                {
                    Data = lst,
                    Message = "succesfull",
                    status = true
                };
            }
        }
        public void AddDaysWor( Days_of_wor2Dto day)
        {
            
            using (dbEntities db = new dbEntities())
            {
                db.days_of_wor2.Add(day.DtoToDal());
                db.SaveChanges();
            }
        }
        public Days_of_wor2Dto Login(ConstraintsforeigenworkerDto c)
        {
            using (dbEntities db = new dbEntities())
            {
                Days_of_wor2Dto co = Days_of_wor2Dto.DalToDto(db.days_of_wor2.FirstOrDefault(d => d.confid == c.confid));
                return co;
            }
        }
        public void Update(int idday, Days_of_wor2Dto d)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (days_of_wor2 day in db.days_of_wor2)

                {
                    if (day.iddayw == idday)
                    {
                        day.iddayw = idday;
                        day.idwor = d.idwor;
                        day.isSunday = d.days[0];
                        day.isManday = d.days[1];
                        day.isThursday = d.days[2];
                        day.isWenthday = d.days[3];
                        day.isTuthday = d.days[4];
                        day.isFriday = d.days[5];
                        day.isShabbat = d.days[6];
                        day.confid = d.confid;
                        day.hourstartw = d.hourstartw;
                        day.hourendw = d.hourendw;
                    }

                }
                db.SaveChanges();

            }
        }
        public bool[] switchs(string[] days)
        {
            string[] dayes = { "שבת", "שישי", "חמישי", "רביעי", "שלישי", "שני", "ראשון" };
            bool[] dayss = { false, false, false, false, false, false, false };
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (dayes[i].Equals(days[j]))
                    {
                        dayss[i] = true;
                        break;
                    }

                }
            }

            return dayss;
        }
    }
}
